using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient
{
    public class SPTransException : Exception
    {
        public SPTransException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public SPTransException(IRestResponse response)
            : base(response.ErrorMessage, response.ErrorException)
        {
            this.Response = response;
        }

        public IRestResponse Response { get; protected set; }
    }
}
