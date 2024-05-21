namespace STW.ProcessingApi.Function.Services;

using Interfaces;
using Microsoft.Extensions.Logging;
using Models;
using Rules.Interfaces;

public class ValidationService : IValidationService
{
    private readonly IEnumerable<IRule> _rules;
    private readonly IEnumerable<IAsyncRule> _asyncRules;
    private readonly ILogger<ValidationService> _logger;

    public ValidationService(IEnumerable<IRule> rules, IEnumerable<IAsyncRule> asyncRules, ILogger<ValidationService> logger)
    {
        _rules = rules;
        _asyncRules = asyncRules;
        _logger = logger;
    }

    public async Task InvokeRulesAsync(SpsCertificate spsCertificate)
    {
        var errorEvents = new List<ErrorEvent>();

        foreach (var rule in _rules.Where(x => x.ShouldInvoke(spsCertificate)))
        {
            rule.Invoke(spsCertificate, errorEvents);
        }

        foreach (var rule in _asyncRules.Where(x => x.ShouldInvoke(spsCertificate)))
        {
            await rule.InvokeAsync(spsCertificate, errorEvents);
        }

        errorEvents.ForEach(x => _logger.LogInformation(x.ErrorMessage));
    }
}
