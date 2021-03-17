using System;

namespace PhoneBook.Api.Dto
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public Guid? PhoneId { get; set; }
        public Guid? EmailId { get; set; }
        public Guid? LocationId { get; set; }
    }
}
