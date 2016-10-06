using Newtonsoft.Json;
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

        protected virtual RestClient CreateClient(string url)
        {
            var restClient = new RestClient(url);

            restClient.CookieContainer = Container;

            return restClient;
        }

        protected virtual RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);

            request.JsonSerializer = new SPTransJsonSerializer();

            return request;
        }

        public virtual T Execute<T>(RestRequest request)
        {
            var restClient = CreateClient(CurrentServiceEndPoint.Url);
            var response = restClient.Execute(request);

            ThrownIfExceptionFound(response, DefaultValidStatus);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        protected virtual void ThrownIfExceptionFound(IRestResponse response, HttpStatusCode[] validStatus)
        {
            if (response.ErrorException != null || !validStatus.Contains(response.StatusCode))
            {
                throw new SPTransException(response);
            }
        }

        public virtual bool Authenticate()
        {
            var request = CreateRequest("/Login/Autenticar?token={token}", Method.POST);

            request.AddUrlSegment("token", Credential.Token);

            return Execute<bool>(request);
        }

        public virtual IEnumerable<Bus> Bus(string terms)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                throw new ArgumentException(nameof(terms));
            }

            var request = CreateRequest("/Linha/Buscar", Method.GET);

            request.AddQueryParameter("termosBusca", terms);

            return Execute<IEnumerable<Bus>>(request);
        }

        public virtual IEnumerable<Bus> BusDetails(long? busLine)
        {
            if (busLine != null && busLine <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(busLine));
            }

            var request = CreateRequest("/Linha/CarregarDetalhes", Method.GET);

            if (busLine != null)
            {
                request.AddQueryParameter("codigoLinha", busLine.ToString());
            }

            return Execute<IEnumerable<Bus>>(request);
        }

        public virtual IEnumerable<Stop> Stop(string terms)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                throw new ArgumentOutOfRangeException(nameof(terms));
            }

            var request = CreateRequest("/Parada/Buscar", Method.GET);

            request.AddQueryParameter("termosBusca", terms);

            return Execute<IEnumerable<Stop>>(request);
        }

        public virtual IEnumerable<Stop> StopPerLine(long busLine)
        {
            if (busLine <= 0)
            {
                throw new ArgumentException(nameof(busLine));
            }

            var request = CreateRequest("/Parada/BuscarParadasPorLinha", Method.GET);

            request.AddQueryParameter("codigoLinha", busLine.ToString());

            return Execute<IEnumerable<Stop>>(request);
        }

        public virtual IEnumerable<Stop> StopPerCorridor(long corridor)
        {
            if (corridor <= 0)
            {
                throw new ArgumentException(nameof(corridor));
            }

            var request = CreateRequest("/Parada/BuscarParadasPorCorredor", Method.GET);

            request.AddQueryParameter("codigoCorredor", corridor.ToString());

            return Execute<IEnumerable<Stop>>(request);
        }

        public virtual IEnumerable<Corridor> Corridor()
        {
            var request = CreateRequest("/Corredor", Method.GET);

            return Execute<IEnumerable<Corridor>>(request);
        }

        public virtual Position BusPosition(long? busLine)
        {
            if (busLine != null && busLine <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(busLine));
            }

            var request = CreateRequest("/Posicao", Method.GET);

            if (busLine != null)
            {
                request.AddQueryParameter("codigoLinha", busLine.ToString());
            }

            return Execute<Position>(request);
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

            var request = CreateRequest("/Previsao", Method.GET);

            request.AddQueryParameter("codigoParada", stopCode.ToString());
            request.AddQueryParameter("codigoLinha", lineCode.ToString());

            return Execute<ForecastWithLine>(request);
        }

        public virtual ForecastWithGeolocation StopForecastPerLine(long lineCode)
        {
            if (lineCode <= 0)
            {
                throw new ArgumentException(nameof(lineCode));
            }

            var request = CreateRequest("/Previsao/Linha", Method.GET);

            request.AddQueryParameter("codigoLinha", lineCode.ToString());

            return Execute<ForecastWithGeolocation>(request);
        }

        public virtual ForecastWithLine StopForecastPerStop(long stopCode)
        {
            if (stopCode <= 0)
            {
                throw new ArgumentException(nameof(stopCode));
            }

            var request = CreateRequest("/Previsao/Parada", Method.GET);

            request.AddQueryParameter("codigoParada", stopCode.ToString());

            return Execute<ForecastWithLine>(request);
        }
    }
}