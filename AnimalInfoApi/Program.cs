using Microsoft.EntityFrameworkCore;
using AnimalInfoApi.Data;
using AnimalInfoApi.Services;
using AnimalInfoApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Cargar el archivo .env manualmente
Env.Load(); 

// Variables de entorno
builder.Configuration.AddEnvironmentVariables();

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Leer cadena de conexión
var connectionString = builder.Configuration["DB_CONNECTION"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IIAService, IAService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();