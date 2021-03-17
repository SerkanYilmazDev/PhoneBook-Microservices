using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class DeletePersonCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }

        public DeletePersonCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
