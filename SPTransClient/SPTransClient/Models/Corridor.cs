using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Models
{
    public class Corridor
    {
        [JsonProperty("CodCorredor")]
        public string CorridorCode { get; set; }
        [JsonProperty("Nome")]
        public string Name { get; set; }
    }
}
