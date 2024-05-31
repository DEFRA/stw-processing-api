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
        var requestBody = await reader.ReadToEndAsync();

        try
        {
            var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(requestBody);

            if (spsCertificate == null)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var errors = await _validator.IsValid(spsCertificate);

            if (errors.Count == 0)
            {
                _logger.LogInformation("Validation passed");
                return request.CreateResponse(HttpStatusCode.OK);
            }

            _logger.LogWarning("Validation failed");
            var response = request.CreateResponse(HttpStatusCode.BadRequest);
            await response.WriteStringAsync(string.Join(", ", errors.Select(error => error.ToString())));
            return response;
        }
        catch (JsonException exception)
        {
            _logger.LogError(exception.Message);
            var response = request.CreateResponse(HttpStatusCode.BadRequest);
            await response.WriteStringAsync(exception.Message);
            return response;
        }
    }
}
