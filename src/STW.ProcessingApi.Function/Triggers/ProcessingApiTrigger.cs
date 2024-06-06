namespace STW.ProcessingApi.Function.Triggers;

using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Models;
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

        var spsCertificate = JsonSerializer.Deserialize<SpsCertificate>(message.Body);
        await _validationService.InvokeRulesAsync(spsCertificate!);
    }
}
