using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class DeletePersonInformationCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid InformationId { get; set; }

        public DeletePersonInformationCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
