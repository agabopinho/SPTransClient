using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPTransClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Tests
{
    [TestClass]
    public class ClientTests
    {
        Credential credential;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.secrets.json", true);

            var configuration = builder.Build();

            credential = new Credential(configuration["appSettings:SPTrans.Api:Token"]);

            if (credential.Token == "REPLACE WITH YOUR TOKEN")
            {
                throw new ArgumentException("appSettings:SPTrans.Api:Token", "Configure your token in appSettings.json");
            }
        }

        [TestMethod()]
        public async Task BusTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var bus = await client.FindBus("terminal campo limpo");

            Assert.IsNotNull(bus);
            Assert.IsTrue(bus.Count() > 0);
        }

        [TestMethod()]
        public async Task BusesDetailsTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var bus = client.BusDetails(1877);

            Assert.IsNotNull(bus);
        }

        [TestMethod()]
        public async Task BusPositionTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var position = await client.BusPosition(1912);

            Assert.IsNotNull(position);
            Assert.IsNotNull(position.Geolocations);
        }

        [TestMethod()]
        public async Task StopTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var stops = await client.FindStop("Av Paulista");

            Assert.IsNotNull(stops);
        }

        [TestMethod()]
        public async Task CorridorTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var corridors = await client.Corridor();

            Assert.IsNotNull(corridors);
        }

        [TestMethod()]
        public async Task StopForecastPerLineTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var line = await client.ForecastPerLine(1877);

            Assert.IsNotNull(line);
            Assert.IsNotNull(line.Stop);
        }

        [TestMethod()]
        public async Task StopForecastPerStopTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var stop = await client.ForecastPerStop(340015740);

            Assert.IsNotNull(stop);
            Assert.IsNotNull(stop.Stop);
        }

        [TestMethod()]
        public async Task StopForecastPerStopAndLineTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var stop = await client.ForecastPerStopAndLine(340015740, 32815);

            Assert.IsNotNull(stop);
            Assert.IsNotNull(stop.Stop);
        }

        [TestMethod()]
        public async Task StopPerCorridorTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var stops = await client.StopPerCorridor(3);

            Assert.IsNotNull(stops);
        }

        [TestMethod()]
        public async Task StopPerLineTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(await client.Authenticate());

            var stops = await client.StopPerLine(32815);

            Assert.IsNotNull(stops);
        }
    }
}