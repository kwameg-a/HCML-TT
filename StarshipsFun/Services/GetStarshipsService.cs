using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StarshipsFun.Infrastructure;
using StarshipsFun.Models;
using StarshipsFun.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarshipsFun.Services
{
    public class GetStarshipsService : IGetStarshipsService
    {
        private const string _startshipsCacheKey = "cachedStarships";
        private readonly IMemoryCache _cache;
        private readonly ILogger<GetStarshipsService> _logger;
        private readonly IStarshipsServiceClient _starShipsServiceClient;

        public GetStarshipsService(
            IMemoryCache cache,
            ILogger<GetStarshipsService> logger,
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
                var apiResponse = await ServiceExecutor.ExecuteAsync<ApiResponse>(
                    _logger, () => _starShipsServiceClient.GetStarshipsAsync(1));

                starships = apiResponse?.Data?.Starships as List<Starship>;

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddDays(1));
                _cache.Set(_startshipsCacheKey, starships, cacheEntryOptions);
            }
            return starships;
        }        
    }
}
