using DotNetEnv;
using ManolovPWS_v2.Api.DependencyInjection;
using ManolovPWS_v2.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add configuration from appsettings.json and environment variables
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Load environment variables from .env file
Env.Load();

// Add service defaults from the hosting environment
builder.AddServiceDefaults();


// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration, connectionString);

// Auth
builder.Services.AddAuthenticationDI(builder.Configuration);
builder.Services.AddAuthorizationDI();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
