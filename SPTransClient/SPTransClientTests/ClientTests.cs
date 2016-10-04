using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPTransClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Tests
{
    [TestClass()]
    public class ClientTests
    {
        Credential credential;

        [TestInitialize]
        public void Initialize()
        {
            credential = new Credential(ConfigurationManager.AppSettings["SPTrans.ApiToken"]);

            if (credential.Token == "{SPTRANS-TOKEN}")
            {
                throw new ConfigurationErrorsException("Configure token with AppSetting Key=SPTrans.ApiToken");
            }
        }

        [TestMethod()]
        public void SearchBusTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(client.Authenticate());

            var listBus = client.SearchBus("terminal campo limpo");

            Assert.IsNotNull(listBus);
            Assert.IsTrue(listBus.Count() > 0);
        }

        [TestMethod()]
        public void BusesDetailsTest()
        {
            var client = new Client(ServiceEndPoint.Production, credential);

            Assert.IsTrue(client.Authenticate());

            var bus = client.BusDetails(35041);

            Assert.IsNotNull(bus);
            Assert.IsTrue(bus.Count() > 0);
        }
    }
}