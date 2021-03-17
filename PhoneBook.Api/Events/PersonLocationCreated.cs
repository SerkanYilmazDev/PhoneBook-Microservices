using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class PersonLocationCreated : IEvent
    {
        public Guid Id { get; set; }
        public string Location { get; set; }

        public PersonLocationCreated(Guid id, string location)
        {
            Id = id;
            Location = location;
        }
    }
}
