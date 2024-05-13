namespace STW.ProcessingApi.Function.Triggers;

using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

public class ProcessingApiTrigger
{
    private readonly ILogger _logger;

    public ProcessingApiTrigger(ILogger<ProcessingApiTrigger> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ProcessingApiTrigger))]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "test")] HttpRequestData request)
    {
        _logger.LogInformation($"{nameof(ProcessingApiTrigger)} function was invoked.");

        var response = request.CreateResponse(HttpStatusCode.OK);

        if (request.Headers.TryGetValues("Content-Type", out var contentTypes))
        {
            response.Headers.Add("Content-Type", contentTypes);
        }

        response.Body = request.Body;

        return response;
    }
}
