using Reporting.Api.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reporting.Api.HttpServices
{
    public interface ILocationHttpService
    {
        Task<Location> GetLocationAsync(Guid id);
        Task<List<Location>> GetLocationsByNameAsync(string locationName);
    }
}
