using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class PersonDeleted : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }

        public PersonDeleted(Guid id, string name, string surname, string companyName)
        {
            Id = id;
            Name = name;
            Surname = surname;
            CompanyName = companyName;
        }

    }
}
