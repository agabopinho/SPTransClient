using Newtonsoft.Json;
using System.Collections.Generic;

namespace SPTransClient.Models
{
    public class ForecastLine : Forecast
    {
        [JsonProperty("vs")]
        public IEnumerable<Geolocation> Geolocations { get; set; }
    }
}
