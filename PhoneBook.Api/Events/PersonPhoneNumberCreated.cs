using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class PersonPhoneNumberCreated : IEvent
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }

        public PersonPhoneNumberCreated(Guid id, string phoneNumber)
        {
            Id = id;
            PhoneNumber = phoneNumber;
        }
    }
}
