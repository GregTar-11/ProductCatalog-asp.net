using ProductCatalog.Services;
using ProductCatalog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        // обработка ошибок инициализации
        Console.WriteLine($"Ошибка инициализации: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
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

// Подключаем Razor Pages (в тому числе для Identity)
app.MapRazorPages();

app.Run();