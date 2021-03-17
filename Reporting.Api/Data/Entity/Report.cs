using Shared.Data;
using System;
using System.Collections.Generic;

namespace Reporting.Api.Data.Entity
{
    public class Report : BaseEntity
    {
        public Guid PersonId { get; set; }
        public List<ReportItem> Items { get; set; }
        public ReportStatus Status { get; set; }
    }
}
