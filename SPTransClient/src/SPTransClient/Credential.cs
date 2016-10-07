using System;

namespace SPTransClient
{
    public class Credential
    {
        public Credential(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(nameof(token));
            }

            this.Token = token;
        }

        public string Token { get; set; }
    }
}
