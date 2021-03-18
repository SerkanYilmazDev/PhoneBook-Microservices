using Shared.Messages;
using System;

namespace Reporting.Api.Events
{
    public class ReportComplated : IEvent
    {
        public Guid Id { get; }

        public ReportComplated(Guid id)
        {
            Id = id;
        }
    }
}
