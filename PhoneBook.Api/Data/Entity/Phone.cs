using Shared.Data;
using System;

namespace PhoneBook.Api.Data.Entity
{
    public class Phone: BaseEntity
    {
        public Guid PersonId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
