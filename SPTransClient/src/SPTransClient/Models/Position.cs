using Newtonsoft.Json;
using SPTransClient.Converters;
using System;
using System.Collections.Generic;

namespace SPTransClient.Models
{
    public class Position
    {
        [JsonProperty("hr")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan? Hour { get; set; }

        [JsonProperty("vs")]
        public IEnumerable<Geolocation> Geolocations { get; set; }
    }
}
