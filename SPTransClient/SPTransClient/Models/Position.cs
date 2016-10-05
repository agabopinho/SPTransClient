using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPTransClient.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Models
{
    public class Position
    {
        [JsonProperty("hr")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Hour { get; set; }
        [JsonProperty("vs")]
        public IEnumerable<Geolocation> Geolocation { get; set; }
    }
}
