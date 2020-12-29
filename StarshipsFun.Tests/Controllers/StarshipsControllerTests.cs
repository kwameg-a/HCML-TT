using Moq;
using Shouldly;
using StarshipsFun.Controllers;
using StarshipsFun.Models;
using StarshipsFun.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StarshipsFun.Tests.Controllers
{
    public class StarshipsControllerTests
    {
        private readonly Mock<IGetStarshipsService> _mockService;

        public StarshipsControllerTests()
        {
           _mockService =  new Mock<IGetStarshipsService>();
        }

        [Fact]
        public async Task GetAction_WhenCalled_ThenShouldReturnAllStarships()
        {
            _mockService.Setup(x => x.ExecuteAsync()).Returns(Task.FromResult((IList<Starship>)new List<Starship>
            {
                new Starship
                {
                    CostInCredits = "900000"
                }
            }));
            var controller = new StarshipsController(_mockService.Object);

            var response = await controller.Get();

            response.ShouldNotBeNull();
            response.Count.ShouldBe(1);         
        }
    }
}
