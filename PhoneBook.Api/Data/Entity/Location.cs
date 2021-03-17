using Shared.Data;
using System;

namespace PhoneBook.Api.Data.Entity
{
    public class Location : BaseEntity
    {
        public Guid PersonId { get; set; }
        public string LocationName { get; set; }
    }
}
