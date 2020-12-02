using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Shouldly;
using StarshipsFun.Models;
using StarshipsFun.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StarshipsFun.Tests.Services
{
    public class GetStarshipsServiceTests
    {
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
        private readonly Mock<IOptionsMonitor<StarWarsApiConfig>> _mockOptionsMonitor;

        public GetStarshipsServiceTests()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockOptionsMonitor = new Mock<IOptionsMonitor<StarWarsApiConfig>>();            
        }

        [Fact]
        public async Task GetStarshipsService_WhenExecuted_WithOKStatusCode_ReturnsListOfStarships()
        {
            // Arrange
            var apiResponseModel = GetApiResponseModel();

            _mockOptionsMonitor.SetupGet(x => x.CurrentValue).Returns(() => new StarWarsApiConfig
            {
                StarshipsBaseUrl = It.IsAny<string>()
            });

            IMemoryCache memoryCache = GetIMemoryCache(apiResponseModel);

            var mockHttpMessageHandler = MockHttpMessageHandler(apiResponseModel, HttpStatusCode.OK);
            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var service = new GetStartshipsService(memoryCache, httpClient, _mockOptionsMonitor.Object);

            // Act
            var result = await service.ExecuteAsync(); ;

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<Starship>>();
            result.Count.ShouldBe(1);
        }

        #region HELPERS
        private static Mock<HttpMessageHandler> MockHttpMessageHandler(ApiResponse apiResponse, HttpStatusCode statusCode)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = statusCode,
                   Content = new StringContent(JsonConvert.SerializeObject(apiResponse), Encoding.UTF8, "application/json")
               });
            return mockHttpMessageHandler;
        }

        private static ApiResponse GetApiResponseModel()
        {
            return new ApiResponse
            {
                StarshipsCount = 1,
                NextPageUrl = "",
                PreviousPageUrl = "",
                Starships = new List<Starship>
                {
                    new Starship
                    {
                        CostInCredits = "100000",
                        HyperdriveRating = "1.0",
                        TopSpeedInMegalights = "1300",
                        Films = new [] {"http://swapi.dev/api/films/3/"},
                        CrewRequired = "5400"
                    }
                }
            };
        }

        private static IMemoryCache GetIMemoryCache(ApiResponse apiResponse)
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            memoryCache.Set("cachedStarships", apiResponse.Starships);
            return memoryCache;
        }
        #endregion
    }
}
