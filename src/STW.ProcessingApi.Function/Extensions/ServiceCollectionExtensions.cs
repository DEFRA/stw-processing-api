namespace STW.ProcessingApi.Function.Extensions;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    }

    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidationService, ValidationService>();
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
