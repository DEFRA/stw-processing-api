namespace STW.ProcessingApi.Function.Triggers;

using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Validation;

public class ProcessingApiTrigger
{
    private readonly ILogger _logger;
    private readonly IRuleValidator _validator;

    public ProcessingApiTrigger(ILogger<ProcessingApiTrigger> logger, IRuleValidator validator)
    {
        _logger = logger;
        _validator = validator;
    }

    [Function(nameof(ProcessingApiTrigger))]
    public async Task Run([ServiceBusTrigger("%ServiceBusQueueName%", Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message)
    {
        _logger.LogInformation($"{nameof(ProcessingApiTrigger)} function was invoked.");

        var messageBody = message.Body.ToString();

        try
        {
            var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(messageBody);

            if (spsCertificate == null)
            {
                _logger.LogWarning("SPSCertificate is null");
                return;
            }

            var errors = await _validator.IsValid(spsCertificate);

            if (errors.Count == 0)
            {
                _logger.LogInformation("Validation passed");
            }

            _logger.LogWarning("Validation failed");
            _logger.LogWarning(string.Join(", ", errors.Select(error => error.ToString())));
        }
        catch (JsonException exception)
        {
            _logger.LogError(exception.Message);
        }
    }
}
