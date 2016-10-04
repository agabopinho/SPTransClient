using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient
{
    public class Client
    {
        protected static readonly HttpStatusCode[] DefaultValidStatus = new[]
        {
            HttpStatusCode.OK,
            HttpStatusCode.Created,
            HttpStatusCode.Accepted
        };

        public Client(ServiceEndPoint endPoint, Credential credential)
        {
            if (endPoint == null)
            {
                throw new ArgumentNullException(nameof(endPoint));
            }

            if (credential == null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            this.CurrentServiceEndPoint = endPoint;
            this.Credential = credential;
        }

        public ServiceEndPoint CurrentServiceEndPoint { get; }

        public Credential Credential { get; }

        public CookieContainer Container { get; } = new CookieContainer();

        protected virtual RestClient CreateClient(ServiceEndPoint Endpoint)
        {
            var restClient = new RestClient(Endpoint.Url);

            restClient.CookieContainer = Container;

            return restClient;
        }

        protected virtual RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);

            request.JsonSerializer = new SPTransJsonSerializer();

            return request;
        }

        protected virtual void ThrownIfExceptionFound(IRestResponse response, HttpStatusCode[] validStatus)
        {
            if (!validStatus.Contains(response.StatusCode) || response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new SPTransException(response);
            }
        }

        public virtual bool Authenticate()
        {
            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Login/Autenticar?token={token}", Method.POST);

            request.AddUrlSegment("token", Credential.Token);

            var response = restClient.Execute<bool>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual IEnumerable<Buses> SearchBus(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentException(nameof(searchTerm));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Linha/Buscar", Method.GET);

            request.AddQueryParameter("termosBusca", searchTerm);

            var response = restClient.Execute<List<Buses>>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual IEnumerable<Buses> BusDetails(int? busLine)
        {
            if (busLine != null && busLine <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(busLine));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Linha/CarregarDetalhes", Method.GET);

            if (busLine != null)
            {
                request.AddQueryParameter("codigoLinha", busLine.ToString());
            }

            var response = restClient.Execute<List<Buses>>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }
    }
}
