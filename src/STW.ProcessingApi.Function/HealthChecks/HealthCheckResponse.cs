namespace STW.ProcessingApi.Function.HealthChecks;

public class HealthCheckResponse
{
    public HealthCheckResponse(string status, string version)
    {
        Status = status;
        Version = version;
    }

    public string Status { get; private set; }

    public string Version { get; private set; }
}
