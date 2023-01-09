using System;
using System.Security.Principal;
using System.Text.Json.Serialization;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using Web.BusinessService;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketReservationsController : ControllerBase
{
    private readonly ILogger<TicketReservationsController> _logger;
    private ITicketReservations _ticketReservations { get; }

    public TicketReservationsController(
        [FromServices] ITicketReservations ticketReservations,
        ILogger<TicketReservationsController> logger)
    {
        _ticketReservations = ticketReservations;
        _logger = logger;
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<TicketReservation>> GetAsync([FromRoute] int id)
    {
        var ticketReservation = await _ticketReservations.GetAsync(id);

        if (ticketReservation == null)
        {
            return NotFound();
        }

        return ticketReservation;
    }


    [HttpPost]
    [Route("")]
    public IActionResult SetAsync([FromBody] TicketReservation ticketReservation)
    {
        _ticketReservations.SetAsync(ticketReservation);

        return Created($"TicketReservations/{ticketReservation.Id}", new { id = ticketReservation.Id });
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteAsync([FromRoute] int id)
    {
        _ticketReservations.DeleteAsync(id);

        return NoContent();
    }
}

