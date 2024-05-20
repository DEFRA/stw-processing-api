using STW.ProcessingApi.Function.Validation;
using STW.ProcessingApi.Function.Validation.Interfaces;
using STW.ProcessingApi.Function.Validation.Rules;

namespace STW.ProcessingApi.Function.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Options;

public static class ServiceCollectionExtensions
{
    public static void RegisterOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<HealthCheckOptions>()
            .Configure<IConfiguration>((s, c) => c.GetSection(HealthCheckOptions.Section).Bind(s));
    }

    public static void RegisterRuleValidator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRuleValidator, Validator>(x =>
        {
            var rules = new List<IRule>
            {
                new ExampleRule()
            };
            var asyncRules = new List<IAsyncRule>
            {
                new ExampleAsyncRule()
            };
            return new Validator(rules, asyncRules);
        });
    }
}
