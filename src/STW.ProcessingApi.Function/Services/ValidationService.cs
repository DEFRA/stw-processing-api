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
        var validationErrors = new List<ValidationError>();

        foreach (var rule in _rules.Where(x => x.ShouldInvoke(spsCertificate)))
        {
            rule.Invoke(spsCertificate, validationErrors);
        }

        if (validationErrors.Count == 0)
        {
            foreach (var rule in _asyncRules.Where(x => x.ShouldInvoke(spsCertificate)))
            {
                await rule.InvokeAsync(spsCertificate, validationErrors);
            }
        }

        if (validationErrors.Count == 0)
        {
            _logger.LogInformation("Validation passed");
        }
        else
        {
            _logger.LogWarning("Validation failed");
            validationErrors.ForEach(x => _logger.LogWarning(x.ErrorMessage));
        }
    }
}
