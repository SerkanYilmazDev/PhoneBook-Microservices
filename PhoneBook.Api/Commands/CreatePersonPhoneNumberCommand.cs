using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class CreatePersonPhoneNumberCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string PhoneNumber { get; set; }

        public CreatePersonPhoneNumberCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
