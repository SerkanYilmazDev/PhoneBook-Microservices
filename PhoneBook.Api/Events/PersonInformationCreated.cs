using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class PersonInformationCreated : IEvent
    {
        public Guid Id { get; set; }
        public string Info { get; set; }

        public PersonInformationCreated(Guid id, string info)
        {
            Id = id;
            Info = info;
        }
    }
}
