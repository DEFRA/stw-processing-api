using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using STW.ProcessingApi.Function.Triggers;
using STW.ProcessingApi.Function.UnitTests.Constants;
using STW.ProcessingApi.Function.UnitTests.TestDoubles;
using STW.ProcessingApi.Function.Validation.Interfaces;

namespace STW.ProcessingApi.Function.UnitTests.Triggers;

[TestClass]
public class ProcessingApiTriggerTests
{
    private Mock<ILogger<ProcessingApiTrigger>> _loggerMock;
    private Mock<IRuleValidator> _validatorMock;
    private ProcessingApiTrigger _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger<ProcessingApiTrigger>>();
        _validatorMock = new Mock<IRuleValidator>();
        _systemUnderTest = new ProcessingApiTrigger(_loggerMock.Object, _validatorMock.Object);
    }

    [TestMethod]
    public void Run_ReturnsResponseWithRequestBodyAndContentType_WhenCalled()
    {
        // Arrange
        const string contentType = "application/json; charset=utf-8";
        var requestBody = new MemoryStream(Encoding.Default.GetBytes("{test}"));
        var httpRequestData = new MockHttpRequestData(Mock.Of<FunctionContext>(), requestBody, HttpVerbs.Post);
        httpRequestData.Headers.Add(HttpHeaders.ContentType, contentType);
        _validatorMock.Setup(v => v.IsValid("{test}"))
            .Returns(true);

        // Act
        var result = _systemUnderTest.Run(httpRequestData).Result;

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Headers.Should().ContainKey(HttpHeaders.ContentType).WhoseValue.Should().Contain(contentType);
        result.Body.Should().BeSameAs(requestBody);

        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Passed"), Times.Once);
    }

    [TestMethod]
    public async Task Run_LogsValidationFailure_ValidationFails()
    {
        // Arrange
        const string contentType = "application/json; charset=utf-8";
        var requestBody = new MemoryStream(Encoding.Default.GetBytes("{}"));
        var httpRequestData = new MockHttpRequestData(Mock.Of<FunctionContext>(), requestBody, HttpVerbs.Post);
        httpRequestData.Headers.Add(HttpHeaders.ContentType, contentType);
        _validatorMock.Setup(v => v.IsValid("{}"))
            .Returns(false);

        // Act
        await _systemUnderTest.Run(httpRequestData);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Failed"), Times.Once);
    }
}
