using System.Threading.Tasks;
using BL.Api;
using BL.Models;
using BL.Services;
using Dal.Api;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    BusyAppointmentForUser _blBusyAppointments;
    private readonly IBLUser _blUser;
    IClient clients;
    ITherapist therapists;
    public AppointmentsController(IDal dal)
    {
        //_blUser = blUser;
        //clients = dal.Clients;
        therapists = dal.Therapists;
    }
    [HttpGet]
    //public async Task<ActionResult<List<Client>>> GetAllClients()
    //{
    //    var result = await clients.ReadAllAsync();
    //    if (result == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(result);
    //}
    public async Task<ActionResult<List<Therapist>>> GetAllTherapists()
    {
        var result = await therapists.ReadAllAsync();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    //public async Task<ActionResult<List<BusyAppointmentForUser>>> GetAllBusyAppointmentsForUser([FromQuery] string id, [FromQuery] string name)
    //{
    //    var result = await _blUser.LogInSpecificUser(id, name);

    //    return Ok(result);
    //}
}
