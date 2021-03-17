using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class DeletePersonCommandHandler : AsyncRequestHandler<DeletePersonCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public DeletePersonCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _dbContext.Persons.Include(p => p.Informations)
                                                   .Include(p => p.Locations)
                                                   .Include(p => p.Phones)
                                                   .Include(p => p.Emails).FirstOrDefaultAsync(s => s.Id == command.PersonId);

            if (person != null)
            {
                _dbContext.Emails.RemoveRange(person.Emails);
                _dbContext.Informations.RemoveRange(person.Informations);
                _dbContext.Phones.RemoveRange(person.Phones);
                _dbContext.Locations.RemoveRange(person.Locations);

                _dbContext.Persons.Remove(person);

                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person deleted.");

            await _busPublisher.PublishAsync(new PersonDeleted(person.Id, person.Name, person.Surname,
                 person.CompanyName), null);

        }
    }
}
