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
        DataStore.Disruptions.Add(new Disruption
        {
            Id = DataStore.Disruptions.Count + 1,
            RouteId = "15",
            Message = "20 minute delay",
            CreatedAt = DateTime.Now
        });

        return Ok("Disruption added");
    }
}