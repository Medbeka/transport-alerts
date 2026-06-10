using System.Text.Json;
using TransportAlerts.DTOs;
using TransportAlerts.Models;

namespace TransportAlerts.Services;

public class DisruptionPollingService : BackgroundService
{
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
                Console.WriteLine(ex.Message);
            }

            await Task.Delay(
                TimeSpan.FromSeconds(30),
                stoppingToken);
        }
    }

    private async Task CheckFeed()
    {
        if (!File.Exists("disruptions.json"))
            return;

        var json =
            await File.ReadAllTextAsync("disruptions.json");

        var disruptions =
            JsonSerializer.Deserialize<List<DisruptionDto>>(json);

        if (disruptions == null)
            return;

        foreach (var item in disruptions)
        {
            bool exists =
                DataStore.Disruptions.Any(d =>
                    d.RouteId == item.RouteId &&
                    d.Message == item.Message);

            if (!exists)
            {
                DataStore.Disruptions.Add(
                    new Disruption
                    {
                        Id = DataStore.Disruptions.Count + 1,
                        RouteId = item.RouteId,
                        Message = item.Message,
                        CreatedAt = DateTime.Now
                    });

                Console.WriteLine(
                    $"Imported route {item.RouteId}");
            }
        }
    }
}