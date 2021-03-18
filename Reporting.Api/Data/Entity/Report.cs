using Shared.Data;
using System;

namespace Reporting.Api.Data.Entity
{
    public class Report : BaseEntity
    {
        public Guid PersonId { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
        public string Name { get; set; }
        public ReportStatus Status { get; set; }
    }
}
