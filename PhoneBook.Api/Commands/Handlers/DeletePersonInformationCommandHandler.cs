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
    public class DeletePersonInformationCommandHandler : AsyncRequestHandler<DeletePersonInformationCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public DeletePersonInformationCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeletePersonInformationCommand command, CancellationToken cancellationToken)
        {
            var personInformation = await _dbContext.Informations.FindAsync(command.InformationId);
            if (personInformation != null)
            {
                _dbContext.Informations.Remove(personInformation);
                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person information deleted.");

            await _busPublisher.PublishAsync(new PersonInformationDeleted(personInformation.Id, personInformation.Info), null);
        }
    }
}
