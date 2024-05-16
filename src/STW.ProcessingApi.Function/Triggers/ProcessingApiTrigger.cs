using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "test")] HttpRequestData request)
    {
        _logger.LogInformation($"{nameof(ProcessingApiTrigger)} function was invoked.");

        using var reader = new StreamReader(request.Body);
        var requestBody = reader.ReadToEndAsync().Result;
        _logger.LogInformation(_validator.IsValid(requestBody)
                ? "Validation Passed"
                : "Validation Failed");

        var response = request.CreateResponse(HttpStatusCode.OK);

        if (request.Headers.TryGetValues("Content-Type", out var contentTypes))
        {
            response.Headers.Add("Content-Type", contentTypes);
        }

        response.Body = request.Body;

        return response;
    }
}
