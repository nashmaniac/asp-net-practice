using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Demo.Dto;
using Demo.Interfaces;

namespace Demo.Services
{
    public class JsonPlaceholderService: IJsonPlaceholder
    {
        public HttpClient _client { get; }
        public JsonPlaceholderService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            _client = client;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var response = await _client.GetAsync("/users");
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<User>>(res); 
        }
    }
}
