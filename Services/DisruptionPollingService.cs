using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TransportAlerts.Data;
using TransportAlerts.DTOs;
using TransportAlerts.Models;

namespace TransportAlerts.Services;

public class DisruptionPollingService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DisruptionPollingService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckFeed();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Poller Error: {ex.Message}");
            }

            await Task.Delay(
                TimeSpan.FromSeconds(30),
                stoppingToken);
        }
    }

    private async Task CheckFeed()
    {
        var filePath = "disruptions.json";

        if (!File.Exists(filePath))
            return;

        var json = await File.ReadAllTextAsync(filePath);

        var disruptions =
            JsonSerializer.Deserialize<List<DisruptionDto>>(json);

        if (disruptions == null)
            return;

        using var scope =
            _scopeFactory.CreateScope();

        var db =
            scope.ServiceProvider
                 .GetRequiredService<AppDbContext>();

        foreach (var item in disruptions)
        {
            bool exists =
                await db.Disruptions.AnyAsync(d =>
                    d.RouteId == item.RouteId &&
                    d.Message == item.Message);

            if (!exists)
            {
                db.Disruptions.Add(new Disruption
                {
                    RouteId = item.RouteId,
                    Message = item.Message,
                    CreatedAt = DateTime.Now
                });

                Console.WriteLine(
                    $"New disruption imported: {item.RouteId}");
            }
        }

        await db.SaveChangesAsync();
    }
}