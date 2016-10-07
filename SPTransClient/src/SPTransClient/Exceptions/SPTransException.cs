using System;
using System.Net.Http;

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
