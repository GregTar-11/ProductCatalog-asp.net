using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCatalog.Data;
using ProductCatalog.Services;

var builder = WebApplication.CreateBuilder(args);

// Реєструємо базу даних функціоналу програми
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Реєструємо окрему базу даних для Identity
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"))
);

// Додаємо Identity з UI, що підтримує ролі
builder
    .Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = false
    )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultUI();

builder.Services.AddTransient<IGreetingService, GreetingService>();
builder.Services.AddControllersWithViews();

// Реєструємо Razor Pages (Identity UI використовує Razor Pages)
builder.Services.AddRazorPages();

var app = builder.Build();

// Глобальний блок обробки помилок із логуванням
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/html";
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception, "Відбулася непередбачена помилка");

            // Приклад простого HTML-відповіді.
            await context.Response.WriteAsync("<html><body>\n");
            await context.Response.WriteAsync(
                "Відбулася помилка. Будь ласка, спробуйте пізніше.<br>\n"
            );
            await context.Response.WriteAsync("<a href='/'>Повернутися на головну</a><br>\n");
            await context.Response.WriteAsync("</body></html>\n");
        });
    });
    app.UseHsts();
}

//
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Помилка ініціалізації бази даних");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Додаємо аутентифікацію перед авторизацією
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// Підключаємо Razor Pages (у тому числі для Identity)
app.MapRazorPages();

app.Run();
