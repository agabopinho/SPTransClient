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
        public TimeSpan TimeLeft { get; set; }

        [JsonProperty("ps")]
        public ForecastLine Stop { get; set; }
    }
}
