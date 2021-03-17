using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class CreatePersonLocationCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Location { get; set; }

        public CreatePersonLocationCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
