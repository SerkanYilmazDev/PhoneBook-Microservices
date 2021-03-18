using Shared.Messages;
using System;

namespace Reporting.Api.Events
{
    public class ReportCreated : IEvent
    {
        public Guid Id { get; }
        public Guid PersonId { get; }
        public Guid LocationId { get; }

        public ReportCreated(Guid id, Guid personId, Guid locationId)
        {
            Id = id;
            PersonId = personId;
            LocationId = locationId;
        }
    }
}
