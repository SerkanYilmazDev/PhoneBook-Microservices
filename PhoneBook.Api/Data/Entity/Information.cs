using Shared.Data;
using System;

namespace PhoneBook.Api.Data.Entity
{
    public class Information : BaseEntity
    {
        public Guid PersonId { get; set; }

        public string Info { get; set; }
    }
}