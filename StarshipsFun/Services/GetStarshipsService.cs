using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StarshipsFun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StarshipsFun.Services
{
    public class GetStarshipsService : IGetStarshipsService
    {
        private const string _startshipsCacheKey = "cachedStarships";
        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;
        private readonly StarWarsApiConfig _starWarsApiConfig;

        public GetStarshipsService(
            IMemoryCache cache,
            HttpClient httpClient,
            IOptionsMonitor<StarWarsApiConfig> starWarsApiConfig)
        {
            _cache = cache;
            _httpClient = httpClient;
            _starWarsApiConfig = starWarsApiConfig.CurrentValue;
        }

        public async Task<IList<Starship>> ExecuteAsync()
        {
            try
            {
                if (!_cache.TryGetValue<List<Starship>>(_startshipsCacheKey, out var starships))
                {
                    starships = new List<Starship>();
                    ApiResponse apiResponse;
                    var pageNumber = 1;
                    do
                    {
                        var jsonString = await _httpClient.GetStringAsync($"{_starWarsApiConfig.StarshipsBaseUrl}?page={pageNumber}");
                        apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);
                        var collection = apiResponse?.Starships;
                        starships.AddRange(collection.Select(starship => starship));
                        pageNumber++;
                    } while (!string.IsNullOrWhiteSpace(apiResponse?.NextPageUrl));

                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddDays(1));
                    _cache.Set(_startshipsCacheKey, starships, cacheEntryOptions);
                }

                Shuffle(starships);

                return starships;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong, please contact the system administrator");
            }
        }

        private static void Shuffle(List<Starship> starships)
        {
            for (int i = 0; i < starships.Count; i++)
            {
                var x = new Random().Next(starships.Count);
                Starship starship = starships[i];
                starships[i] = starships[x];
                starships[x] = starship;
            }
        }
    }
}
