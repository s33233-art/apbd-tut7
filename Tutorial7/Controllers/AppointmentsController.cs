using Microsoft.AspNetCore.Mvc;
using Tutorial7.Services;

namespace Tutorial7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    public AppointmentsController(IAppointmentService service)
    {
        _appointmentService = service;
    }
}