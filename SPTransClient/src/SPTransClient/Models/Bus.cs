using Newtonsoft.Json;
using SPTransClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Models
{
    public class Bus
    {
        [JsonProperty("CodigoLinha")]
        public long? LineCode { get; set; }

        [JsonProperty("Circular")]
        public bool? IsCircular { get; set; }

        [JsonProperty("Letreiro")]
        public string LabelNumber { get; set; }

        [JsonProperty("Sentido")]
        public DirectionTypes? Direction { get; set; }

        [JsonProperty("Tipo")]
        public BusType? Type { get; set; }

        [JsonProperty("DenominacaoTPTS")]
        public string NameDirectionToSecundary { get; set; }

        [JsonProperty("DenominacaoTSTP")]
        public string NameDirectionToPrimary { get; set; }

        [JsonProperty("Informacoes")]
        public string Informations { get; set; }
    }
}
