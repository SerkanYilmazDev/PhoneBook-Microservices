using Shared.Data;
using System;

namespace Reporting.Api.Data.Entity
{
    public class ReportItem : BaseEntity
    {
        public Guid ReportId { get; set; }
        public Guid LocationId { get; set; }
        public string Name { get; set; }
    }
}
