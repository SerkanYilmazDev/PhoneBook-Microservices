using Shared.Data;
using System.Collections.Generic;

namespace PhoneBook.Api.Data.Entity
{
    public class Person : BaseEntity
    {
		public string Name { get; set; }
		public string Surname { get; set; }
		public string CompanyName { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Email> Emails { get; set; }
        public List<Location> Locations { get; set; }
        public List<Information> Informations { get; set; }
    }
}
