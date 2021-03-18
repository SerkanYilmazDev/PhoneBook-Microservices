using Microsoft.Extensions.Logging;
using Reporting.Api.Data;
using Reporting.Api.HttpServices;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.Api.Events.Handlers
{
    public class ReportCreatedHandler : IEventHandler<ReportCreated>
    {
        private readonly ReportDbContext _dbContext;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<ReportCreatedHandler> _logger;
        private readonly IPersonHttpService _personHttpService;
        private readonly ILocationHttpService _locationHttpService;

        public ReportCreatedHandler(ReportDbContext dbContext,
                            IBusPublisher busPublisher,
                            ILogger<ReportCreatedHandler> logger,
                            IPersonHttpService personHttpService,
                            ILocationHttpService locationHttpService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _busPublisher = busPublisher;
            _personHttpService = personHttpService;
            _locationHttpService = locationHttpService;
        }

        public async Task HandleAsync(ReportCreated _event, ICorrelationContext context)
        {
            var report = await _dbContext.Reports.FindAsync(_event.Id);

            try
            {
                var location = await _locationHttpService.GetLocationAsync(report.LocationId);
                var locations = await _locationHttpService.GetLocationsByNameAsync(location.LocationName);
                var phoneNumbers = new List<Phone>();

                foreach (var locationItem in locations)
                {
                    phoneNumbers.AddRange(await _personHttpService.GetPhoneNumbersByPersonIdAsync(locationItem.PersonId));
                }

                report.LocationName = location.LocationName;
                report.PersonCount = locations.Select(l => l.PersonId).Distinct().Count();
                report.PhoneNumberCount = phoneNumbers.Select(p => p.PhoneNumber).Distinct().Count();
                report.Status = Data.Entity.ReportStatus.Completed;
                report.UpdateDate = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"[Local Transaction] : Report completed. CorrelationId: {context.CorrelationId}");
            }
            catch (Exception ex)
            {
                report.Status = Data.Entity.ReportStatus.Failed;
                report.UpdateDate = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"[Local Transaction] : Report failed. CorrelationId: {context.CorrelationId}");

                throw ex;
            }
        }
    }
}
