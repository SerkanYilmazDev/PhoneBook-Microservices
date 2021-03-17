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
    public class CreatePersonPhoneNumberCommandHandler : AsyncRequestHandler<CreatePersonPhoneNumberCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public CreatePersonPhoneNumberCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(CreatePersonPhoneNumberCommand command, CancellationToken cancellationToken)
        {
            var person = await _dbContext.Persons.FindAsync(command.PersonId);

            if (person != null)
            {
                _dbContext.Phones.Add(new Data.Entity.Phone
                {
                    Id = command.Id,
                    PersonId = person.Id,
                    PhoneNumber = command.PhoneNumber
                });

                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person phone number created.");

            await _busPublisher.PublishAsync(new PersonPhoneNumberCreated(person.Id, person.Name), null);
        }
    }
}
