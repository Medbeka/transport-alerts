namespace TransportAlerts.Models;

public class Disruption
{
    public int Id { get; set; }

    public string RouteId { get; set; } = "";

    public string Message { get; set; } = "";

    public DateTime CreatedAt { get; set; }
}