### Simples SPTrans API Cliente

#### SPTrans Portal Desenvolvedores

* [http://www.sptrans.com.br/desenvolvedores/] (http://www.sptrans.com.br/desenvolvedores/)

#### Nuget

* [.NET 4.5, 4.6 e .NET Core] (https://www.nuget.org/packages/SPTransClient/)

#### Exemplo de uso
```
var credential = new Credential("YOUR CREDENTIAL");
var client = new Client(ServiceEndPoint.Production, credential);

// necessário para API
await client.Authenticate();

// busca ônibus
var bus = await client.FindBus("terminal campo limpo");
```
