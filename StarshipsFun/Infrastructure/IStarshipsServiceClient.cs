using System.Net.Http;
using System.Threading.Tasks;

namespace StarshipsFun.Infrastructure
{
    public interface IStarshipsServiceClient
    {
        Task<HttpResponseMessage> GetStarshipsAsync(int pageNumber);
    }
}