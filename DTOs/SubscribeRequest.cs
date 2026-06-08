namespace TransportAlerts.DTOs;

public class SubscribeRequest
{
    public string Email { get; set; } = "";

    public string RouteId { get; set; } = "";
}