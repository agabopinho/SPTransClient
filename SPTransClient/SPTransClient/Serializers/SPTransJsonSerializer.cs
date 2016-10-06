using Newtonsoft.Json;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient
{
    public class SPTransJsonSerializer : ISerializer
    {
        public SPTransJsonSerializer()
        {
        }

        public virtual string DateFormat { get; set; }
        public virtual string RootElement { get; set; }
        public virtual string Namespace { get; set; }
        public virtual string ContentType { get; set; }

        public virtual string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
