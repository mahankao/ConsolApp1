using ConsoleApp1.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Настройка политики CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5275", policy =>
    {
        policy.WithOrigins("http://localhost:5275")
            .AllowAnyMethod()                  
            .AllowAnyHeader();                  
    });
});

// Добавление DbContext для работы с бд
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(9,1,0)))); 

builder.Services.AddControllers(); // Включает работу API

builder.Services.AddEndpointsApiExplorer(); // Помогает увидеть маршруты
builder.Services.AddSwaggerGen(); // Создаёт интерфейс для тестирования и документацию

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Включение политики CORS
app.UseCors("AllowLocalhost5275"); 

// Использование HTTPS
app.UseHttpsRedirection();

app.MapControllers();

app.Run();