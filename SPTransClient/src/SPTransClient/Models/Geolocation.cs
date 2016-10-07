using Newtonsoft.Json;
using SPTransClient.Converters;
using System;

namespace SPTransClient.Models
{
    public class Geolocation
    {
        [JsonProperty("p")]
        public long? PositionCode { get; set; }

        [JsonProperty("t")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan? ScheduledTime { get; set; }

        [JsonProperty("a")]
        public bool? IsLate { get; set; }

        [JsonProperty("py")]
        public float? Longitude { get; set; }

        [JsonProperty("px")]
        public float? Latitude { get; set; }
    }
}
