```
var client = new Client(ServiceEndPoint.Production, credential);

Assert.IsTrue(client.Authenticate());

var bus = client.Bus("terminal campo limpo");

Assert.IsNotNull(bus);
Assert.IsTrue(bus.Count() > 0);
```