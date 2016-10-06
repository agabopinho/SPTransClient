using Newtonsoft.Json;
using SPTransClient.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Models
{
    public class ForecastWithLine
    {
        [JsonProperty("hr")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Hour { get; set; }

        [JsonProperty("p")]
        public ForecastStop Stop { get; set; }
    }
}
