using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient
{
    public class Credential
    {
        public Credential(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("token");
            }

            this.Token = token;
        }

        public string Token { get; set; }
    }
}
