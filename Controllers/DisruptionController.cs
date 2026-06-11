using Microsoft.AspNetCore.Mvc;
using TransportAlerts.Models;
using TransportAlerts.Services;

namespace TransportAlerts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DisruptionController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(DataStore.Disruptions);
    }

    [HttpPost("seed")]
    public IActionResult Seed()
    {
        DataStore.Disruptions.Clear();

        DataStore.Disruptions.Add(new Disruption
        {
            Id = 1,
            RouteId = "15",
            Message = "20 minute delay",
            CreatedAt = DateTime.Now
        });

        DataStore.Disruptions.Add(new Disruption
        {
            Id = 2,
            RouteId = "22",
            Message = "Service cancelled",
            CreatedAt = DateTime.Now
        });

        return Ok("Demo data loaded");
    }
}