using Shared.Messages;
using System;

namespace PhoneBook.Api.Commands
{
    public class CreatePersonReportsByLocationCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string LocationId { get; set; }

        public CreatePersonReportsByLocationCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
