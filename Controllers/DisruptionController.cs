using Microsoft.AspNetCore.Mvc;
using TransportAlerts.Data;
using TransportAlerts.Models;

namespace TransportAlerts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DisruptionController : ControllerBase
{
    private readonly AppDbContext _db;

    public DisruptionController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_db.Disruptions.ToList());
    }

    [HttpPost("seed")]
    public IActionResult Seed()
    {
        _db.Disruptions.Add(new Disruption
        {
            RouteId = "15",
            Message = "20 minute delay",
            CreatedAt = DateTime.Now
        });

        _db.SaveChanges();

        return Ok("Disruption added");
    }
}