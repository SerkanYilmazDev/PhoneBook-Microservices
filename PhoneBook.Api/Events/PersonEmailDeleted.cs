using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class PersonEmailDeleted : IEvent
    {
        public Guid Id { get; set; }
        public string EmailAdress { get; set; }

        public PersonEmailDeleted(Guid id, string emailAdress)
        {
            Id = id;
            EmailAdress = emailAdress;
        }
    }
}
