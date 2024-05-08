namespace STW.ProcessingApi.Function.Triggers;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using HealthCheck;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Options;

[ExcludeFromCodeCoverage]
public class HealthCheckTrigger
{
    private readonly HealthCheckService _healthCheckService;
    private readonly HealthCheckOptions _healthCheckOptions;

    public HealthCheckTrigger(HealthCheckService healthCheckService, IOptions<HealthCheckOptions> healthCheckOptions)
    {
        _healthCheckService = healthCheckService;
        _healthCheckOptions = healthCheckOptions.Value;
    }

    [Function(nameof(HealthCheckTrigger))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequestData request)
    {
        var healthReport = await _healthCheckService.CheckHealthAsync();

        var response = request.CreateResponse(HttpStatusCode.OK);

        var healthCheckBody = new HealthCheckResponse(healthReport.Status.ToString(), _healthCheckOptions.Version ?? "Unknown");

        await response.WriteAsJsonAsync(healthCheckBody);

        return response;
    }
}
