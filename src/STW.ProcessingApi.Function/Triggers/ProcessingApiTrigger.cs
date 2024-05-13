using System.Text;
using STW.ProcessingApi.Function.Validation;
using STW.ProcessingApi.Function.Validation.Rule;

namespace STW.ProcessingApi.Function.Triggers;

using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

public class ProcessingApiTrigger
{
    private readonly ILogger _logger;
    private readonly IValidator _validator;

    public ProcessingApiTrigger(ILogger<ProcessingApiTrigger> logger, IValidator validator)
    {
        _logger = logger;
        _validator = validator;
    }

    [Function(nameof(ProcessingApiTrigger))]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "test")] HttpRequestData request)
    {
        _logger.LogInformation($"{nameof(ProcessingApiTrigger)} function was invoked.");
        using (var reader = new StreamReader(request.Body, Encoding.UTF8))
        {
            var requestBody = reader.ReadToEnd();
            var validationRules = new List<IRule> { new ExampleRule() };
            var asyncValidationRules = new List<IAsyncRule> { new ExampleAsyncRule() };
            _logger.LogInformation(_validator.IsValid(validationRules, asyncValidationRules, requestBody)
                ? "Validation Passed"
                : "Validation Failed");
        }

        var response = request.CreateResponse(HttpStatusCode.OK);

        if (request.Headers.TryGetValues("Content-Type", out var contentTypes))
        {
            response.Headers.Add("Content-Type", contentTypes);
        }

        response.Body = request.Body;

        return response;
    }
}
