using Microsoft.EntityFrameworkCore;
using AnimalInfoApi.Data;
using AnimalInfoApi.Services;
using AnimalInfoApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Variables de entorno
builder.Configuration.AddEnvironmentVariables();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

app.UseRouting();
app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    if (!db.Animales.Any())
    {
        db.Animales.AddRange(
            new Animal
            {
                Nombre = "León",
                Especie = "Panthera leo",
                Habitat = "Sabana",
                Descripcion = "Gran felino carnívoro, conocido como 'el rey de la selva'.",
                ImagenUrl = "https://example.com/leon.jpg"
            },
            new Animal
            {
                Nombre = "Tortuga Marina",
                Especie = "Chelonia mydas",
                Habitat = "Océanos tropicales",
                Descripcion = "Tortuga marina herbívora que migra largas distancias.",
                ImagenUrl = "https://example.com/tortuga.jpg"
            }
        );
        db.SaveChanges();
    }
}

app.Run();
