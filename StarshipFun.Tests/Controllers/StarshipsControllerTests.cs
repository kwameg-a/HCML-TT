using Moq;
using StarshipsFun.Controllers;
using StarshipsFun.Models;
using StarshipsFun.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarshipFun.Tests.Controllers
{
    public class StarshipsControllerTests
    {
        private readonly Mock<IGetStartshipsService> _mockService;

        public StarshipsControllerTests()
        {
           _mockService =  new Mock<IGetStartshipsService>();
        }

        public Task GetAction_WhenCalled_ThenShouldReturn()
        {
            var controller = new StarshipsController(_mockService.Object);
            _mockService.Setup(x => x.ExecuteAsync()).Returns(() => Task.FromResult(new IEnumerable<Starship>()));

        }

        private object GetResponse()
        {
            throw new NotImplementedException();
        }
    }
}
