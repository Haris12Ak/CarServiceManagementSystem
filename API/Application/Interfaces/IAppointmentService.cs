using Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAppointmentService
    {
        Task CreatePublicAppointmentAsync(string companySlug, PublicAppointmentRequest request, CancellationToken cancellationToken);
        Task CreateAppointmentAsync(AppointmentRequest request, CancellationToken cancellationToken);

    }
}
