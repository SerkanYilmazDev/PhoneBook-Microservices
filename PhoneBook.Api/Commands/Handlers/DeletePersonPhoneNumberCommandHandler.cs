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
    public class DeletePersonPhoneNumberCommandHandler : AsyncRequestHandler<DeletePersonPhoneNumberCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public DeletePersonPhoneNumberCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeletePersonPhoneNumberCommand command, CancellationToken cancellationToken)
        {
            var personPhoneNumber = await _dbContext.Phones.FindAsync(command.PhoneNumberId);
            if (personPhoneNumber != null)
            {
                _dbContext.Phones.Remove(personPhoneNumber);
                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person phone number deleted.");

            await _busPublisher.PublishAsync(new PersonPhoneNumberDeleted(personPhoneNumber.Id, personPhoneNumber.PhoneNumber), null);
        }
    }
}
