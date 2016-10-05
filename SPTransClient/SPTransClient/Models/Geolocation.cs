using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Models
{
    public class Geolocation
    {
        [JsonProperty("p")]
        public long? StopCode { get; set; }
        // TODO: verificar o que é esse campo
        //[JsonProperty("a")]
        //public bool? A { get; set; } 
        [JsonProperty("py")]
        public float? Longitude { get; set; }
        [JsonProperty("px")]
        public float? Latitude { get; set; }
    }
}
