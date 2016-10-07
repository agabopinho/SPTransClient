using Newtonsoft.Json;

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
