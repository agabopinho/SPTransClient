using Newtonsoft.Json;
using SPTransClient.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Models
{
    public class ForecastWithGeolocation
    {
        [JsonProperty("hr")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Hour { get; set; }

        [JsonProperty("ps")]
        public IEnumerable<ForecastLine> Stop { get; set; }
    }
}
