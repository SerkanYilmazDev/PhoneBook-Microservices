using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class DeletePersonPhoneNumberCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid PhoneNumberId { get; set; }

        public DeletePersonPhoneNumberCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
