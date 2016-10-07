### Simples SPTrans API Cliente

#### Nuget

* [.NET 4.5 e .NET Core] (https://www.nuget.org/packages/SPTransClient/)

#### Exemplo de uso
```
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
public void BusTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var bus = client.Bus("terminal campo limpo");

    Assert.IsNotNull(bus);
    Assert.IsTrue(bus.Count() > 0);
}

[TestMethod()]
public void BusesDetailsTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var bus = client.BusDetails(1877);

    Assert.IsNotNull(bus);
}

[TestMethod()]
public void BusPositionTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var position = client.BusPosition(1912);

    Assert.IsNotNull(position);
    Assert.IsNotNull(position.Geolocations);
}

[TestMethod()]
public void StopTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var stops = client.Stop("Av Paulista");

    Assert.IsNotNull(stops);
}

[TestMethod()]
public void CorridorTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var corridors = client.Corridor();

    Assert.IsNotNull(corridors);
}

[TestMethod()]
public void StopForecastPerLineTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var line = client.StopForecastPerLine(1877);

    Assert.IsNotNull(line);
    Assert.IsNotNull(line.Stop);
}

[TestMethod()]
public void StopForecastPerStopTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var stop = client.StopForecastPerStop(340015740);

    Assert.IsNotNull(stop);
    Assert.IsNotNull(stop.Stop);
}

[TestMethod()]
public void StopForecastPerStopAndLineTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var stop = client.StopForecastPerStopAndLine(340015740, 32815);

    Assert.IsNotNull(stop);
    Assert.IsNotNull(stop.Stop);
}

[TestMethod()]
public void StopPerCorridorTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var stops = client.StopPerCorridor(3);

    Assert.IsNotNull(stops);
}

[TestMethod()]
public void StopPerLineTest()
{
    var client = new Client(ServiceEndPoint.Production, credential);

    Assert.IsTrue(client.Authenticate());

    var stops = client.StopPerLine(32815);

    Assert.IsNotNull(stops);
}
```
