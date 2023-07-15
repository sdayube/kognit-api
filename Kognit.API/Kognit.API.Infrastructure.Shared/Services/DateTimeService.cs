using Kognit.API.Application.Interfaces;
using System;

namespace Kognit.API.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}