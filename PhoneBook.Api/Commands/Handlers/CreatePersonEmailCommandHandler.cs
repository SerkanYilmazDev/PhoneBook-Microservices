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
    public class CreatePersonEmailCommandHandler : AsyncRequestHandler<CreatePersonEmailCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public CreatePersonEmailCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(CreatePersonEmailCommand command, CancellationToken cancellationToken)
        {
            var person = await _dbContext.Persons.FindAsync(command.PersonId);

            if (person != null)
            {
                _dbContext.Emails.Add(new Data.Entity.Email
                {
                    Id = command.Id,
                    PersonId = person.Id,
                    EmailAdress = command.EmailAdress
                });

                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person email created.");

            await _busPublisher.PublishAsync(new PersonEmailCreated(person.Id, person.Name), null);
        }
    }
}
