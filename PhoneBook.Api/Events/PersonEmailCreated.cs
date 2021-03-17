using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class PersonEmailCreated : IEvent
    {
        public Guid Id { get; set; }
        public string EmailAdress { get; set; }

        public PersonEmailCreated(Guid id, string emailAdress)
        {
            Id = id;
            EmailAdress = emailAdress;
        }
    }
}
