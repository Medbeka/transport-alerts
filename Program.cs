using Microsoft.EntityFrameworkCore;
using TransportAlerts.Data;
using TransportAlerts.Services;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=transport.db"));

// Controllers
builder.Services.AddControllers();

builder.Services.AddHostedService<DisruptionPollingService>();

// Swagger/OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();