using Newtonsoft.Json;
using SPTransClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SPTransClient
{
    public class Client
    {
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

        public CookieContainer Cookies { get; } = new CookieContainer();

        public virtual HttpClient CreateClient(string url)
        {
            var handle = new HttpClientHandler
            {
                CookieContainer = Cookies
            };

            var client = new HttpClient(handle, true);
            client.BaseAddress = new Uri(CurrentServiceEndPoint.Url);

            return client;
        }

        public async virtual Task<T> Execute<T>(HttpRequestMessage request)
        {
            using (var client = CreateClient(CurrentServiceEndPoint.Url))
            using (var response = await client.SendAsync(request))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception inner)
                {
                    throw new SPTransException(response, inner);
                }

                var stringComtent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(stringComtent);
            }
        }

        public async virtual Task<bool> Authenticate()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"Login/Autenticar?token={Credential.Token}");

            return await Execute<bool>(request);
        }

        public async virtual Task<IEnumerable<Bus>> Bus(string terms)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                throw new ArgumentException(nameof(terms));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"Linha/Buscar?termosBusca={terms}");

            return await Execute<IEnumerable<Bus>>(request);
        }

        public async virtual Task<IEnumerable<Bus>> BusDetails(long busLine)
        {
            if (busLine <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(busLine));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"Linha/CarregarDetalhes?codigoLinha={busLine}");

            return await Execute<IEnumerable<Bus>>(request);
        }

        public async virtual Task<IEnumerable<Stop>> Stop(string terms)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                throw new ArgumentOutOfRangeException(nameof(terms));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"Parada/Buscar?termosBusca={terms}");

            return await Execute<IEnumerable<Stop>>(request);
        }

        public async virtual Task<IEnumerable<Stop>> StopPerLine(long busLine)
        {
            if (busLine <= 0)
            {
                throw new ArgumentException(nameof(busLine));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"Parada/BuscarParadasPorLinha?codigoLinha={busLine}");

            return await Execute<IEnumerable<Stop>>(request);
        }

        public async virtual Task<IEnumerable<Stop>> StopPerCorridor(long corridor)
        {
            if (corridor <= 0)
            {
                throw new ArgumentException(nameof(corridor));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"Parada/BuscarParadasPorCorredor?codigoCorredor={corridor}");

            return await Execute<IEnumerable<Stop>>(request);
        }

        public async virtual Task<IEnumerable<Corridor>> Corridor()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "Corredor");

            return await Execute<IEnumerable<Corridor>>(request);
        }

        public async virtual Task<Position> BusPosition(long busLine)
        {
            if (busLine <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(busLine));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"Posicao?codigoLinha={busLine}");

            return await Execute<Position>(request);
        }

        public async virtual Task<ForecastWithLine> StopForecastPerStopAndLine(long stopCode, long lineCode)
        {
            if (stopCode <= 0)
            {
                throw new ArgumentException(nameof(stopCode));
            }

            if (lineCode <= 0)
            {
                throw new ArgumentException(nameof(lineCode));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"Previsao?codigoParada={stopCode}&codigoLinha={lineCode}");

            return await Execute<ForecastWithLine>(request);
        }

        public async virtual Task<ForecastWithGeolocation> StopForecastPerLine(long lineCode)
        {
            if (lineCode <= 0)
            {
                throw new ArgumentException(nameof(lineCode));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"Previsao/Linha?codigoLinha={lineCode}");

            return await Execute<ForecastWithGeolocation>(request);
        }

        public async virtual Task<ForecastWithLine> StopForecastPerStop(long stopCode)
        {
            if (stopCode <= 0)
            {
                throw new ArgumentException(nameof(stopCode));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"Previsao/Parada?codigoParada={stopCode}");

            return await Execute<ForecastWithLine>(request);
        }
    }
}