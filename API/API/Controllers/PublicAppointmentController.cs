using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Application.Requests;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("{companySlug}")]
    [ApiController]
    public class PublicAppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IServiceTypeService _serviceTypeService;

        public PublicAppointmentController(
            IAppointmentService appointmentService,
            IServiceTypeService serviceTypeService)
        {
            _appointmentService = appointmentService;
            _serviceTypeService = serviceTypeService;
        }

        [HttpGet("services")]
        public async Task<List<ServiceTypeDto>> GetAllServices([Required] string companySlug, CancellationToken cancellationToken)
        {
            var serviceTypes = await _serviceTypeService.GetByCompanySlugAsync(companySlug, cancellationToken);

            return serviceTypes.ToDto();
        }

        [HttpPost("create-appointment")]
        public async Task<IActionResult> CreateAppointment([Required] string companySlug, [FromBody] PublicAppointmentRequest request, CancellationToken cancellationToken)
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
