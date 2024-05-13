using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STW.ProcessingApi.Function.Extensions;
using STW.ProcessingApi.Function.Validation;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(
        serviceCollection =>
        {
            serviceCollection.RegisterOptions();
            serviceCollection.AddHealthChecks();
            serviceCollection.AddScoped<IValidator, Validator>();
        })
    .Build();

host.Run();
