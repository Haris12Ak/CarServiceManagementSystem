using Application.Interfaces;
using Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CrateAppointment([FromBody] AppointmentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _appointmentService.CreateAppointmentAsync(request, cancellationToken);

                return Ok(new { Message = "You have successfully booked an appointment." });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "The appointment was unsuccessful.",
                    Error = ex.Message
                });
            }
        }
    }
}
