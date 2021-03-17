using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneBook.Api.Dto;

namespace PhoneBook.Api.HttpServices
{
    public class PersonHttpService : IPersonHttpService
    {
        private HttpClient _client { get; }

        public PersonHttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<PersonDto> GetPersonByCompanyName(string companyName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"persons/{companyName}");
            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                response.Content.Dispose();
                return JsonConvert.DeserializeObject<PersonDto>(content);
            }

            return null;
        }
    }
}
