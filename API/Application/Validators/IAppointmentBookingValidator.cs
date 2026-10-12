using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{
    public interface IAppointmentBookingValidator
    {
        Task<int> ValidateAsync(Companies company, DateTime scheduledAt, int estimatedDuration, CancellationToken cancellationToken);
    }
}
