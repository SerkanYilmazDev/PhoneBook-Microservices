using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class PersonLocationDeleted : IEvent
    {
        public Guid Id { get; set; }
        public string Location { get; set; }

        public PersonLocationDeleted(Guid id, string location)
        {
            Id = id;
            Location = location;
        }
    }
}
