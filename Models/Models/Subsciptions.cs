namespace TransportAlerts.Models;

public class Subscription
{
    public int Id { get; set; }

    public string RouteId { get; set; } = "";

    public int UserId { get; set; }

    public User? User { get; set; }
}