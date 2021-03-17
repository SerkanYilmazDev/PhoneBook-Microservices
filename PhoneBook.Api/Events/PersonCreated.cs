using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Events
{
    public class PersonCreated : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }

        public PersonCreated(Guid id, string name, string surname, string companyName)
        {
            Id = id;
            Name = name;
            Surname = surname;
            CompanyName = companyName;
        }
    }
}
