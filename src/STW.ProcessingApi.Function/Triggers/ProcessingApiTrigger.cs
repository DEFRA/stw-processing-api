using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using STW.ProcessingApi.Function.Validation.Interfaces;

namespace STW.ProcessingApi.Function.Triggers;

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
    public async Task Run([ServiceBusTrigger("%ServiceBusQueueName%", Connection = "ServiceBusConnectionString", IsSessionsEnabled = true)]
        ServiceBusReceivedMessage message)
    {
        _logger.LogInformation($"{nameof(ProcessingApiTrigger)} function was invoked.");

        var messageBody = message.Body.ToString();

        var isValid = await _validator.IsValidAsync(messageBody);

        _logger.LogInformation("Validation {result}", isValid ? "Passed" : "Failed");
    }
}
