using Shared.Messages;
using System;

namespace Reporting.Api.Events
{
    public class ReportFailed : IEvent
    {
        public Guid OrderId { get; set; }
        public ReportFailed(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}
