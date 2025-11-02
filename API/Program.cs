using Application.DependencyInjection;
using Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.Api.DependencyInjection;
using Shared.Application.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// Shared.Api
builder.Services.AddSharedApiLayer();

// Agregar el Logger.
builder.Host.UseSerilog();

// Application
builder.Services.AddApplicationLayer(Assembly.GetExecutingAssembly());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Infrastructure
builder.Services.AddInfrastructureLayer(connectionString!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Habilita servir archivos estáticos desde wwwroot
app.UseStaticFiles();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Loggear inicio de aplicación.
try
{
    Log.Information("Starting up the application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program 
{
}