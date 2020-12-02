using Newtonsoft.Json;
using System.Collections.Generic;

namespace StarshipsFun.Models
{
    public class ApiResponse
    {
        [JsonProperty("count")]
        public int StarshipsCount { get; set; }

        [JsonProperty("next")]
        public string NextPageUrl { get; set; }

        [JsonProperty("previous")]
        public string PreviousPageUrl { get; set; }

        [JsonProperty("results")]
        public IList<Starship> Starships { get; set; }
    }
}
