using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBook.Api.Data;
using PhoneBook.Api.Events;
using Shared.RabbitMq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Api.Commands.Handlers
{
    public class DeletePersonEmailCommandHandler : AsyncRequestHandler<DeletePersonEmailCommand>
    {
        private readonly PhoneBookDbContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public DeletePersonEmailCommandHandler(PhoneBookDbContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeletePersonEmailCommand command, CancellationToken cancellationToken)
        {
            var personEmail = await _dbContext.Emails.FindAsync(command.EmailId);
            if (personEmail != null)
            {
                _dbContext.Emails.Remove(personEmail);
                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person email deleted.");

            await _busPublisher.PublishAsync(new PersonEmailDeleted(personEmail.Id, personEmail.EmailAdress), null);
        }
    }
}
