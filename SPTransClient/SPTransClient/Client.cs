using RestSharp;
using SPTransClient.Models;
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

        public virtual IEnumerable<Bus> Bus(string terms)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                throw new ArgumentException(nameof(terms));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Linha/Buscar", Method.GET);

            request.AddQueryParameter("termosBusca", terms);

            var response = restClient.Execute<List<Bus>>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual IEnumerable<Bus> BusDetails(long? busLine)
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

            var response = restClient.Execute<List<Bus>>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual IEnumerable<Stop> BusStop(string terms)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                throw new ArgumentOutOfRangeException(nameof(terms));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Parada/Buscar", Method.GET);

            request.AddQueryParameter("termosBusca", terms);

            var response = restClient.Execute<List<Stop>>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual IEnumerable<Stop> StopPerLine(long busLine)
        {
            if (busLine <= 0)
            {
                throw new ArgumentException(nameof(busLine));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Parada/BuscarParadasPorLinha", Method.GET);

            request.AddQueryParameter("codigoLinha", busLine.ToString());

            var response = restClient.Execute<List<Stop>>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual IEnumerable<Stop> StopPerCorridor(long corridor)
        {
            if (corridor <= 0)
            {
                throw new ArgumentException(nameof(corridor));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Parada/BuscarParadasPorCorredor", Method.GET);

            request.AddQueryParameter("codigoCorredor", corridor.ToString());

            var response = restClient.Execute<List<Stop>>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual IEnumerable<Corridor> Corridor()
        {
            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Corredor", Method.GET);

            var response = restClient.Execute<List<Corridor>>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual Position BusPosition(long? busLine)
        {
            if (busLine != null && busLine <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(busLine));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Posicao", Method.GET);

            if (busLine != null)
            {
                request.AddQueryParameter("codigoLinha", busLine.ToString());
            }

            var response = restClient.Execute<Position>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual ForecastWithLine StopForecastPerStopAndLine(long stopCode, long lineCode)
        {
            if (stopCode <= 0)
            {
                throw new ArgumentException(nameof(stopCode));
            }

            if (lineCode <= 0)
            {
                throw new ArgumentException(nameof(lineCode));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Previsao", Method.GET);

            request.AddQueryParameter("codigoParada", stopCode.ToString());
            request.AddQueryParameter("codigoLinha", lineCode.ToString());

            var response = restClient.Execute<ForecastWithLine>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual ForecastWithGeolocation StopForecastPerLine(long lineCode)
        {
            if (lineCode <= 0)
            {
                throw new ArgumentException(nameof(lineCode));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Previsao/Linha", Method.GET);

            request.AddQueryParameter("codigoLinha", lineCode.ToString());

            var response = restClient.Execute<ForecastWithGeolocation>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }

        public virtual ForecastWithLine StopForecastPerStop(long stopCode)
        {
            if (stopCode <= 0)
            {
                throw new ArgumentException(nameof(stopCode));
            }

            var restClient = CreateClient(CurrentServiceEndPoint);
            var request = CreateRequest("/Previsao/Parada", Method.GET);

            request.AddQueryParameter("codigoParada", stopCode.ToString());

            var response = restClient.Execute<ForecastWithLine>(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return response.Data;
        }
    }
}