namespace STW.ProcessingApi.Function.Triggers;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

[ExcludeFromCodeCoverage]
public class HealthCheckTrigger
{
    private readonly HealthCheckService _healthCheckService;

    public HealthCheckTrigger(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [Function(nameof(HealthCheckTrigger))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequestData request)
    {
        var healthReport = await _healthCheckService.CheckHealthAsync();

        var response = request.CreateResponse(HttpStatusCode.OK);

        await response.WriteStringAsync(healthReport.Status.ToString());

        return response;
    }
}
