using StarshipsFun.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarshipsFun.Services
{
    public interface IGetStarshipsService
    {
        Task<IList<Starship>> ExecuteAsync();
    }
}