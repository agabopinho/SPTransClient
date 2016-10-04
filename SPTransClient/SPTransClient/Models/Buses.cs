using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient
{
    public class Buses
    {
        public int? CodigoLinha { get; set; }
        public bool? Circular { get; set; }
        public string Letreiro { get; set; }
        public int? Sentido { get; set; }
        public int? Tipo { get; set; }
        public string DenominacaoTPTS { get; set; }
        public string DenominacaoTSTP { get; set; }
        public string Informacoes { get; set; }
    }
}
