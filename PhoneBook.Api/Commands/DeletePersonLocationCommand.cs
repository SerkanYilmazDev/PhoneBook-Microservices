using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class DeletePersonLocationCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid LocationId { get; set; }

        public DeletePersonLocationCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
