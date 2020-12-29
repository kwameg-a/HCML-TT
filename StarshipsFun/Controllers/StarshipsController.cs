using Microsoft.AspNetCore.Mvc;
using StarshipsFun.Models;
using StarshipsFun.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarshipsFun.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StarshipsController : ControllerBase
    {
        private readonly IGetStarshipsService _getStarshipsService;

        public StarshipsController(IGetStarshipsService getStarshipsService) => _getStarshipsService = getStarshipsService;

        [HttpGet]
        public Task<IList<Starship>> Get() => _getStarshipsService.ExecuteAsync();
    }
}
