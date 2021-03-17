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
    public class CreatePersonLocationCommandHandler : AsyncRequestHandler<CreatePersonLocationCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public CreatePersonLocationCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(CreatePersonLocationCommand command, CancellationToken cancellationToken)
        {
            var person = await _dbContext.Persons.FindAsync(command.PersonId);

            if (person != null)
            {
                _dbContext.Locations.Add(new Data.Entity.Location
                {
                    Id = command.Id,
                    PersonId = person.Id,
                    LocationName = command.Location
                });

                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person location created.");

            await _busPublisher.PublishAsync(new PersonLocationCreated(person.Id, person.Name), null);
        }
    }
}
