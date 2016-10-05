using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Models
{
    public class Stop
    {
        [JsonProperty("CodigoParada")]
        public long? StopCode { get; set; }
        [JsonProperty("Nome")]
        public string Name { get; set; }
        [JsonProperty("Endereco")]
        public string Address { get; set; }
        [JsonProperty("Latitude")]
        public float? Latitude { get; set; }
        [JsonProperty("Longitude")]
        public float? Longitude { get; set; }
    }
}
