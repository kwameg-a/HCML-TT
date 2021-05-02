using Microsoft.AspNetCore.Mvc;
using StarshipsFun.Models;
using StarshipsFun.Services;
using StarshipsFun.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarshipsFun.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StarshipsController : ControllerBase
    {
        private readonly IGetAllStarshipsService _getAllStarshipsService;
        private readonly IGetStarshipsService _getStarshipsService;

        public StarshipsController(
            IGetAllStarshipsService getAllStarshipsService,
            IGetStarshipsService getStarshipsService)
        {
            _getAllStarshipsService = getAllStarshipsService;
            _getStarshipsService = getStarshipsService;
        }

        [HttpGet]
        public Task<IList<Starship>> Get() => _getStarshipsService.ExecuteAsync();

        [HttpGet("all")]
        public Task<IList<Starship>> GetAllStarships() => _getAllStarshipsService.ExecuteAsync();
    }
}
