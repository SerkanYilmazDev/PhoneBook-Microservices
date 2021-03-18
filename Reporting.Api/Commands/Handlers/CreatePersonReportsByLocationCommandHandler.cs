using Microsoft.Extensions.Logging;
using Reporting.Api.Data;
using Reporting.Api.Data.Entity;
using Reporting.Api.Events;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reporting.Api.Commands.Handlers
{
    public class CreatePersonReportsByLocationCommandHandler : ICommandHandler<CreatePersonReportsByLocationCommand>
    {
        private readonly ReportDbContext _dbContext;
        private readonly ILogger<CreatePersonReportsByLocationCommand> _logger;
        private readonly IBusPublisher _busPublisher;

        public CreatePersonReportsByLocationCommandHandler(ReportDbContext dbContext,
                                         ILogger<CreatePersonReportsByLocationCommand> logger,
                                         IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        public async Task HandleAsync(CreatePersonReportsByLocationCommand command, ICorrelationContext context)
        {
            var reportItems = new List<ReportItem>() {
                                            new ReportItem
                                            {
                                                Id = Guid.NewGuid(),
                                                LocationId = command.LocationId,
                                                ReportId = command.PersonId,
                                                Name = typeof(CreatePersonReportsByLocationCommand).Name
                                            }
            };

            var report = new Report
            {
                Status = ReportStatus.Created,
                Id = Guid.NewGuid(),
                PersonId = command.PersonId,
                Items = reportItems
            };

            _dbContext.Reports.Add(report);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"[Local Transaction] : Report is creating... CorrelationId: {context.CorrelationId}");

            await _busPublisher.PublishAsync(new ReportCreated(report.Id, personId: command.PersonId, locationId: command.LocationId), context);
        }
    }
}
