using System;

namespace SPTransClient
{
    public class ServiceEndPoint
    {
        public static readonly ServiceEndPoint Production = new ServiceEndPoint("http://api.olhovivo.sptrans.com.br/v0/");

        public ServiceEndPoint(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(nameof(url));
            }

            this.Url = url;
        }

        public string Url { get; set; }
    }
}
