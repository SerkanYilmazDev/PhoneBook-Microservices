using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Commands
{
    public class CreatePersonEmailCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string EmailAdress { get; set; }

        public CreatePersonEmailCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
