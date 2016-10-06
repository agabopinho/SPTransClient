using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient
{
    public class SPTransException : Exception
    {
        public SPTransException(HttpResponseMessage response, Exception innerException)
            : base("SPTrans Exception: View inner exception", innerException)
        {
            this.HttpResponse = response;
        }

        public HttpResponseMessage HttpResponse { get; protected set; }
    }
}
