using Soccer.BLL.Interfaces;
using Soccer.BLL.Services;
using Soccer.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Отримуємо рядок підключення з файлу конфігурації
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
if (connection != null)
{
    builder.Services.AddSoccerContext(connection);
}

builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddSoccerAutoMapper();

// Додаємо сервіси MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(); // Обробляє запити до файлів у папці wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teams}/{action=Index}/{id?}");

app.Run();