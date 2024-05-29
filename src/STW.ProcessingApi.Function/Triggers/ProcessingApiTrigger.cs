using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using STW.ProcessingApi.Function.Models;
using STW.ProcessingApi.Function.Validation;

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
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData request)
    {
        _logger.LogInformation($"{nameof(ProcessingApiTrigger)} function was invoked.");

        using var reader = new StreamReader(request.Body);
        var requestBody = reader.ReadToEnd();

        try
        {
            var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(requestBody);

            if (spsCertificate == null)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }

            List<ValidationError> errors = await _validator.IsValid(spsCertificate);

            _logger.LogInformation(errors.Count == 0
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
        catch (JsonException exception)
        {
            _logger.LogError(exception.Message);
            var response = request.CreateResponse(HttpStatusCode.BadRequest);
            response.WriteString(exception.Message);
            return response;
        }
    }
}
