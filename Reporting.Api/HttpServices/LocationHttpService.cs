using Newtonsoft.Json;
using Reporting.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reporting.Api.HttpServices
{
    public class LocationHttpService : ILocationHttpService
    {
        private HttpClient _client { get; }

        public LocationHttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Location> GetLocationAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"locations/{id}");
            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                response.Content.Dispose();
                return JsonConvert.DeserializeObject<Data.Location>(content);

            }
            throw new Exception("Location service connection error");
        }

        public async Task<List<Location>> GetLocationsByNameAsync(string locationName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"locations/{locationName}/name");
            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                response.Content.Dispose();
                return JsonConvert.DeserializeObject<List<Data.Location>>(content);
            }
            throw new Exception("Location service connection error");
        }

    }
}
