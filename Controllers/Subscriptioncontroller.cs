using Microsoft.AspNetCore.Mvc;
using TransportAlerts.DTOs;
using TransportAlerts.Models;
using TransportAlerts.Services;

namespace TransportAlerts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    [HttpPost]
    public IActionResult Subscribe(SubscribeRequest request)
    {
        var user = new User
        {
            Id = DataStore.Users.Count + 1,
            Email = request.Email
        };

        DataStore.Users.Add(user);

        var subscription = new Subscription
        {
            Id = DataStore.Subscriptions.Count + 1,
            RouteId = request.RouteId,
            UserId = user.Id
        };

        DataStore.Subscriptions.Add(subscription);

        return Ok("Subscribed!");
    }

    [HttpGet]
    public IActionResult GetSubscriptions()
    {
        return Ok(DataStore.Subscriptions);
    }

    [HttpGet("alerts")]
    public IActionResult GetAlerts()
    {
        var alerts =
            from s in DataStore.Subscriptions
            join u in DataStore.Users on s.UserId equals u.Id
            join d in DataStore.Disruptions on s.RouteId equals d.RouteId
            select new
            {
                Email = u.Email,
                Route = d.RouteId,
                Message = d.Message
            };

        return Ok(alerts.ToList());
    }
}