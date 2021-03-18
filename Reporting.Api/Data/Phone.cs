using System;

namespace Reporting.Api.Data
{
    public class Phone
    {
        public Guid PersonId { get; set; }
        public string PhoneNumber { get; set; }
        public Guid Id { get; set; }
    }
}
