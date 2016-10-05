using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Models
{
    public class ForecastLine : Forecast
    {
        [JsonProperty("vs")]
        public IEnumerable<Geolocation> Geolocations { get; set; }
    }
}
