namespace STW.ProcessingApi.Function.Extensions;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Options;
using Rules.Interfaces;
using Services;
using Services.Interfaces;

public static class ServiceCollectionExtensions
{
    public static void RegisterOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<HealthCheckOptions>()
            .Configure<IConfiguration>((o, c) => c.GetSection(HealthCheckOptions.Section).Bind(o));
        serviceCollection.AddOptions<ApiConfigOptions>()
            .Configure<IConfiguration>((o, c) => c.GetSection(ApiConfigOptions.Section).Bind(o));
    }

    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidationService, ValidationService>();
        serviceCollection.AddScoped<IApprovedEstablishmentService, ApprovedEstablishmentService>();
    }

    public static void RegisterHttpClients(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IApprovedEstablishmentService, ApprovedEstablishmentService>(
            (serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<ApiConfigOptions>>().Value;

                client.BaseAddress = new Uri(options.ApprovedEstablishmentBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(options.Timeout);
            });
    }

    public static void RegisterRules(this IServiceCollection serviceCollection)
    {
        var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

        foreach (var type in assemblyTypes.Where(x => typeof(IRule).IsAssignableFrom(x) && x is { IsClass: true, IsAbstract: false }))
        {
            serviceCollection.AddScoped(typeof(IRule), type);
        }

        foreach (var type in assemblyTypes.Where(x => typeof(IAsyncRule).IsAssignableFrom(x) && x is { IsClass: true, IsAbstract: false }))
        {
            serviceCollection.AddScoped(typeof(IAsyncRule), type);
        }
    }
}
