namespace STW.ProcessingApi.Function.Triggers;

using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Services.Interfaces;

public class ProcessingApiTrigger
{
    private readonly ILogger _logger;
    private readonly IValidationService _validationService;

    public ProcessingApiTrigger(ILogger<ProcessingApiTrigger> logger, IValidationService validationService)
    {
        _logger = logger;
        _validationService = validationService;
    }

    [Function(nameof(ProcessingApiTrigger))]
    public async Task Run(
        [ServiceBusTrigger("%ServiceBusQueueName%", Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message)
    {
        _logger.LogInformation($"{nameof(ProcessingApiTrigger)} function was invoked.");

        var messageBody = message.Body.ToString();

        try
        {
            var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(messageBody);

            var errors = await _validationService.InvokeRulesAsync(spsCertificate!);

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
