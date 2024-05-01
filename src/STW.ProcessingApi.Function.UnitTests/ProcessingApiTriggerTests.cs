namespace STW.ProcessingApi.UnitTests;

using System.Net;
using System.Text;
using Constants;
using FluentAssertions;
using Function;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestDoubles;

[TestClass]
public class ProcessingApiTriggerTests
{
    private Mock<ILogger<ProcessingApiTrigger>> _logger;
    private ProcessingApiTrigger _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _logger = new Mock<ILogger<ProcessingApiTrigger>>();
        _systemUnderTest = new ProcessingApiTrigger(_logger.Object);
    }

    [TestMethod]
    public void Run_ReturnsResponseWithRequestBodyAndContentType_WhenCalled()
    {
        // Arrange
        const string contentType = "application/json; charset=utf-8";
        var requestBody = new MemoryStream(Encoding.Default.GetBytes("{}"));
        var httpRequestData = new MockHttpRequestData(Mock.Of<FunctionContext>(), requestBody, HttpVerbs.Post);
        httpRequestData.Headers.Add(HttpHeaders.ContentType, contentType);

        // Act
        var result = _systemUnderTest.Run(httpRequestData);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Headers.Should().ContainKey(HttpHeaders.ContentType).WhoseValue.Should().Contain(contentType);
        result.Body.Should().BeSameAs(requestBody);

        _logger.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."));
    }
}