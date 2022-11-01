using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsProcessor.Cosmos;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddSingleton<CosmosDbConn>();
    })
    .Build();

host.Run();
