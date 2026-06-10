using TransportAlerts.Models;

namespace TransportAlerts.Services;

public static class DataStore
{
    public static List<User> Users = new();

    public static List<Subscription> Subscriptions = new();

    public static List<Disruption> Disruptions = new();
}