using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Models
{
    public class ForecastStop : Forecast
    {
        [JsonProperty("l")]
        public IEnumerable<Line> Lines { get; set; }
    }
}
