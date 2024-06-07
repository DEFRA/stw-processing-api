using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STW.ProcessingApi.Function.Extensions;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(
        serviceCollection =>
        {
            serviceCollection.RegisterOptions();
            serviceCollection.AddHealthChecks();
            serviceCollection.RegisterHttpClients();
            serviceCollection.RegisterServices();
            serviceCollection.RegisterRules();
            serviceCollection.RegisterHttpClients();
        })
    .Build();

host.Run();
