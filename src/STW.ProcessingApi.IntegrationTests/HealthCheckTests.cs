namespace STW.ProcessingApi.IntegrationTests;

using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class HealthCheckTests
{
    private Process _funcProcess;
    private HttpClient _httpClient;

    [TestInitialize]
    public async Task InitializeAsync()
    {
        _funcProcess = StartAzureFunctionsHost();
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:7071")
        };

        await WaitForHostReady();
    }

    [TestCleanup]
    public void Dispose()
    {
        // Stop the Azure Functions host process
        if (_funcProcess.HasExited)
        {
            return;
        }

        _funcProcess.Kill();
        _funcProcess.Dispose();
    }

    [TestMethod]
    public async Task TestFunctionEndpoint()
    {
        var some = await _httpClient.GetAsync("/api/health");

        Console.WriteLine("hello world");
    }

    private static Process StartAzureFunctionsHost()
    {
        // Start the Azure Functions host process programmatically
        var process = new Process
        {
            StartInfo =
            {
                FileName = "func",
                Arguments = "host start --port 7071",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        process.Start();

        return process;
    }

    private async Task WaitForHostReady()
    {
        // Implement logic to wait for the host to be ready
        // This could involve checking for certain log messages indicating readiness
        // or waiting for a specific amount of time
        await Task.Delay(TimeSpan.FromSeconds(10));
    }
}
