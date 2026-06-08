using Microsoft.EntityFrameworkCore;
using TransportAlerts.Models;

namespace TransportAlerts.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public DbSet<Disruption> Disruptions => Set<Disruption>();
}