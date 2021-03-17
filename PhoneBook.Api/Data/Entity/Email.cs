using Shared.Data;
using System;

namespace PhoneBook.Api.Data.Entity
{
    public class Email : BaseEntity
    {
        public Guid PersonId { get; set; }
        public string EmailAdress { get; set; }
    }
}
