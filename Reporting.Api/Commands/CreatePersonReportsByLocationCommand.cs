using Shared.Messages;
using System;

namespace Reporting.Api.Commands
{
    public class CreatePersonReportsByLocationCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid LocationId { get; set; }

        public CreatePersonReportsByLocationCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
