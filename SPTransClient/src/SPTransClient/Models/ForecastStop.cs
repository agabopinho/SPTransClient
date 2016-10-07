using Newtonsoft.Json;
using System.Collections.Generic;

namespace SPTransClient.Models
{
    public class ForecastStop : Forecast
    {
        [JsonProperty("l")]
        public IEnumerable<Line> Lines { get; set; }
    }
}
