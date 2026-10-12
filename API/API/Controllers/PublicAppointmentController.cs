using Application.Interfaces;
using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("companies/{companySlug}/appointments")]
    [ApiController]
    public class PublicAppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public PublicAppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(string companySlug, [FromBody] PublicAppointmentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _appointmentService.CreatePublicAppointmentAsync(companySlug, request, cancellationToken);

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
