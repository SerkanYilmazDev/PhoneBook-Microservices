using Reporting.Api.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reporting.Api.HttpServices
{
    public interface IPersonHttpService
    {
        Task<List<Phone>> GetPhoneNumbersByPersonIdAsync(Guid id);
    }
}
