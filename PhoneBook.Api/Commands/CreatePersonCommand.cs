using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class CreatePersonCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public Guid? PhoneId { get; set; }
        public Guid? EmailId { get; set; }
        public Guid? LocationId { get; set; }

        public CreatePersonCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
