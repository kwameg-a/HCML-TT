using Microsoft.Extensions.Options;
using StarshipsFun.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace StarshipsFun.Infrastructure
{
    public class StarshipsServiceClient : IStarshipsServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly StarWarsApiConfig _starWarsApiConfig;

        public StarshipsServiceClient(HttpClient httpClient, IOptionsMonitor<StarWarsApiConfig> starWarsApiConfig)
        {
            _httpClient = httpClient;
            _starWarsApiConfig = starWarsApiConfig.CurrentValue;
        }

        public Task<HttpResponseMessage> GetStarshipsAsync(int pageNumber) => 
            _httpClient.GetAsync($"{_starWarsApiConfig.StarshipsBaseUrl}?page={pageNumber}");
    }
}
