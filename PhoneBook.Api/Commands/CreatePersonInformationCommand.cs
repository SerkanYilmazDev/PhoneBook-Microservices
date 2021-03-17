using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class CreatePersonInformationCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Info { get; set; }

        public CreatePersonInformationCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
