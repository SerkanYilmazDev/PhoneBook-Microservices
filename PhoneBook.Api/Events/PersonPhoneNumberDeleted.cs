using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class PersonPhoneNumberDeleted : IEvent
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }

        public PersonPhoneNumberDeleted(Guid id, string phoneNumber)
        {
            Id = id;
            PhoneNumber = phoneNumber;
        }
    }
}
