using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StarshipsFun.Infrastructure;
using StarshipsFun.Models;
using StarshipsFun.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarshipsFun.Services
{
    public class GetAllStarshipsService : IGetAllStarshipsService
    {
        private const string _startshipsCacheKey = "cachedStarships";
        private readonly IMemoryCache _cache;
        private readonly ILogger<GetAllStarshipsService> _logger;
        private readonly IStarshipsServiceClient _starShipsServiceClient;

        public GetAllStarshipsService(
            IMemoryCache cache, 
            ILogger<GetAllStarshipsService> logger,
            IStarshipsServiceClient starShipsServiceClient)
        {
            _cache = cache;
            _logger = logger;
            _starShipsServiceClient = starShipsServiceClient;
        }

        public async ValueTask<IList<Starship>> ExecuteAsync()
        {
            if (!_cache.TryGetValue<List<Starship>>(_startshipsCacheKey, out var starships))
            {
                starships = new List<Starship>();
                ServiceResponse<ApiResponse> apiResponse;
                var pageNumber = 1;
                do
                {
                    apiResponse = await ServiceExecutor.ExecuteAsync<ApiResponse>(
                        _logger, () => _starShipsServiceClient.GetStarshipsAsync(pageNumber));

                    var collection = apiResponse?.Data?.Starships;
                    starships.AddRange(collection.Select(starship => starship));
                    pageNumber++;
                } while (!string.IsNullOrWhiteSpace(apiResponse?.Data.NextPageUrl));

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddDays(1));
                _cache.Set(_startshipsCacheKey, starships, cacheEntryOptions);
            }
            return starships;
        }
    }
}
