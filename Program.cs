using ProductCatalog.Services;
using ProductCatalog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем базу данных функционала приложения
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем отдельную базу данных для Identity
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection")));

// Добавляем Identity с UI, поддерживающим роли
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultUI();

builder.Services.AddTransient<IGreetingService, GreetingService>();
builder.Services.AddControllersWithViews();

// Регистрируем Razor Pages (Identity UI использует Razor Pages)
builder.Services.AddRazorPages();

var app = builder.Build();

// Глобальный блок обработки ошибок с логированием
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
            logger.LogError(exception, "Произошла непредвиденная ошибка");
            
            // Пример простого HTML‑ответа.
            await context.Response.WriteAsync("<html><body>\n");
            await context.Response.WriteAsync("Произошла ошибка. Пожалуйста, попробуйте позже.<br>\n");
            await context.Response.WriteAsync("<a href='/'>Вернуться на главную</a><br>\n");
            await context.Response.WriteAsync("</body></html>\n");
        });
    });
    app.UseHsts();
}

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
        logger.LogError(ex, "Ошибка инициализации базы данных");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Добавляем аутентификацию перед авторизацией
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Подключаем Razor Pages (в том числе для Identity)
app.MapRazorPages();

app.Run();