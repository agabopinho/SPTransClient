using Newtonsoft.Json;

namespace SPTransClient.Models
{
    public class Forecast
    {
        [JsonProperty("cp")]
        public long? StopCode { get; set; }

        [JsonProperty("np")]
        public string StopName { get; set; }

        [JsonProperty("py")]
        public float? Longitude { get; set; }

        [JsonProperty("px")]
        public float? Latitude { get; set; }
    }
}
