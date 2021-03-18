using Newtonsoft.Json;
using Reporting.Api.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reporting.Api.HttpServices
{
    public class PersonHttpService : IPersonHttpService
    {
        private HttpClient _client { get; }

        public PersonHttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Phone>> GetPhoneNumbersByPersonIdAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"persons/{id}/phoneNumbers");
            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                response.Content.Dispose();
                return JsonConvert.DeserializeObject<List<Data.Phone>>(content);
            }
            throw new Exception("Person service connection error");
        }
    }
}
