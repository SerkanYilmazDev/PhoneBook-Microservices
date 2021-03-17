using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBook.Api.Data;
using PhoneBook.Api.Events;
using Shared.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Api.Commands.Handlers
{
    public class DeletePersonLocationCommandHandler : AsyncRequestHandler<DeletePersonLocationCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public DeletePersonLocationCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeletePersonLocationCommand command, CancellationToken cancellationToken)
        {
            var personLocation = await _dbContext.Locations.FindAsync(command.LocationId);
            if (personLocation != null)
            {
                _dbContext.Locations.Remove(personLocation);
                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person location deleted.");

            await _busPublisher.PublishAsync(new PersonLocationDeleted(personLocation.Id, personLocation.LocationName), null);
        }
    }
}
