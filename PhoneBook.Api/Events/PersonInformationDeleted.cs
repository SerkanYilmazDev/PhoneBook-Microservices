using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class PersonInformationDeleted : IEvent
    {
        public Guid Id { get; set; }
        public string Info { get; set; }

        public PersonInformationDeleted(Guid id, string info)
        {
            Id = id;
            Info = info;
        }
    }
}
