using Microsoft.AspNetCore.Mvc;
using TransportAlerts.Data;
using TransportAlerts.DTOs;
using TransportAlerts.Models;

namespace TransportAlerts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly AppDbContext _db;

    public SubscriptionController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult Subscribe(SubscribeRequest request)
    {
        var user = new User
        {
            Email = request.Email
        };

        _db.Users.Add(user);
        _db.SaveChanges();

        var subscription = new Subscription
        {
            RouteId = request.RouteId,
            UserId = user.Id
        };

        _db.Subscriptions.Add(subscription);
        _db.SaveChanges();

        return Ok("Subscribed!");
    }
    [HttpGet("alerts")]
public IActionResult GetAlerts()
{
    var alerts =
        from s in _db.Subscriptions
        join u in _db.Users on s.UserId equals u.Id
        join d in _db.Disruptions on s.RouteId equals d.RouteId
        select new
        {
            Email = u.Email,
            Route = d.RouteId,
            Message = d.Message
        };

    return Ok(alerts.ToList());
}
}