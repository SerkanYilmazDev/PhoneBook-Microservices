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
    public class CreatePersonInformationCommandHandler : AsyncRequestHandler<CreatePersonInformationCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public CreatePersonInformationCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(CreatePersonInformationCommand command, CancellationToken cancellationToken)
        {
            var person = await _dbContext.Persons.FindAsync(command.PersonId);

            if (person != null)
            {
                _dbContext.Informations.Add(new Data.Entity.Information
                {
                    Id = command.Id,
                    PersonId = person.Id,
                    Info = command.Info
                });

                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person information created.");

            await _busPublisher.PublishAsync(new PersonInformationCreated(person.Id, person.Name), null);
        }
    }
}
