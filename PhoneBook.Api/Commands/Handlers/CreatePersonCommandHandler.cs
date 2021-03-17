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
    public class CreatePersonCommandHandler : AsyncRequestHandler<CreatePersonCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public CreatePersonCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            _dbContext.Persons.Add(new Data.Entity.Person
            {
                Id = command.Id,
                Name = command.Name,
                Surname = command.Surname,
                CompanyName = command.CompanyName,
            });

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"[Local Transaction] : Person created.");

            await _busPublisher.PublishAsync(new PersonCreated(command.Id, command.Name, command.Surname,
                 command.CompanyName), null);
        }
    }
}
