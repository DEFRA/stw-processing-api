namespace STW.ProcessingApi.Function.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Options;
using Services;
using Validation;
using Validation.Rules;

public static class ServiceCollectionExtensions
{
    public static void RegisterOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<HealthCheckOptions>()
            .Configure<IConfiguration>((s, c) => c.GetSection(HealthCheckOptions.Section).Bind(s));
    }

    public static void RegisterRuleValidator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<Rule, ExampleRule>();

        serviceCollection.AddScoped<AsyncRule, ExampleAsyncRule>();
        serviceCollection.AddScoped<AsyncRule, BcpValidRule>();

        serviceCollection.AddScoped<IRuleValidator, Validator>();
    }

    public static void RegisterHttpClients(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IBcpService, BcpService>(
            httpClient =>
            {
                httpClient.BaseAddress = new Uri("http://localhost:5295");
            });
    }
}
