using Newtonsoft.Json;
using SPTransClient.Models.Enums;
using System.Collections.Generic;

namespace SPTransClient.Models
{
    public class Line
    {
        [JsonProperty("c")]
        public string LabelNumber { get; set; }

        [JsonProperty("cl")]
        public long? LineCode { get; set; }

        [JsonProperty("sl")]
        public DirectionTypes? Direction { get; set; }

        [JsonProperty("lt0")]
        public string NameDirectionToSecundary { get; set; }

        [JsonProperty("lt1")]
        public string NameDirectionToPrimary { get; set; }

        [JsonProperty("qv")]
        public int? QuantityGeolocations  { get; set; }

        [JsonProperty("vs")]
        public IEnumerable<Geolocation> Geolocations { get; set; }
    }
}
