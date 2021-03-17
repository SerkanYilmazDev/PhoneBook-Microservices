using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reporting.Api.Data;
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

        public ReportCreatedHandler(ReportDbContext dbContext,
                            IBusPublisher busPublisher,
                            ILogger<ReportCreatedHandler> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(ReportCreated _event, ICorrelationContext context)
        {
            var report = await _dbContext.Reports.FindAsync(_event.Id);

            try
            {
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
