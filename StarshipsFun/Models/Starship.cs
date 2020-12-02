using Newtonsoft.Json;

namespace StarshipsFun.Models
{
    public class Starship
    {
        [JsonProperty("cost_in_credits")]
        public string CostInCredits { get; set; }

        [JsonProperty("hyperdrive_rating")]
        public string HyperdriveRating { get; set; }

        [JsonProperty("max_atmosphering_speed")]
        public string TopSpeedInMegalights { get; set; }

        public string[] Films { get; set; }

        [JsonProperty("crew")]
        public string CrewRequired { get; set; }
    }
}
