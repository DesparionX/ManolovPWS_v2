using DotNetEnv;
using ManolovPWS_v2.Api.DependencyInjection;
using ManolovPWS_v2.Api.Extensions;
using ManolovPWS_v2.Infrastructure.DependencyInjection;
using Scalar.AspNetCore;

// Load environment variables from .env file
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings.json and environment variables
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Get the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("manolovdb_local")
        ?? throw new InvalidOperationException("Connection string 'manolovdb_local' not found.");

// Add service defaults from the hosting environment
builder.AddServiceDefaults();


// Add services to the container.
builder.Services.AddExceptionHandlers();
builder.Services.AddApiServices();
builder.Services.AddInfrastructure(builder.Configuration, connectionString);
builder.Services.AddApplication();

// Auth
builder.Services.AddAuthenticationDI(builder.Configuration);
builder.Services.AddAuthorizationDI();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddConfiguredOpenApi();
builder.Services.AddApiCors(builder.Configuration);

var app = builder.Build();

// Using all exception handlers.
app.UseExceptionHandler();

// Seed initial data
await app.SeedDataAsync();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .AddPreferredSecuritySchemes("Bearer")
            .AddHttpAuthentication("Bearer", auth =>
            {
                auth.Token = string.Empty;
            });
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
