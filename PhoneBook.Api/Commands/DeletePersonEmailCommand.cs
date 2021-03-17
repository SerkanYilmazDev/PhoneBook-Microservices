using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class DeletePersonEmailCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid EmailId { get; set; }

        public DeletePersonEmailCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
