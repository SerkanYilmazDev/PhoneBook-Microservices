using PhoneBook.Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.HttpServices
{
    public interface IPersonHttpService
    {
        Task<PersonDto> GetPersonByCompanyName(string companyName);

    }
}
