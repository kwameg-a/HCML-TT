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
        private readonly IGetStartshipsService _getStartshipsService;

        public StarshipsController(IGetStartshipsService getStartshipsService) => _getStartshipsService = getStartshipsService;

        [HttpGet]
        public Task<IList<Starship>> Get() => _getStartshipsService.ExecuteAsync();
    }
}
