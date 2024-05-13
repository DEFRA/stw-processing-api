using STW.ProcessingApi.Function.Validation;
using STW.ProcessingApi.Function.Validation.Rule;

namespace STW.ProcessingApi.Function.UnitTests.Triggers;

using System.Net;
using System.Text;
using Constants;
using FluentAssertions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using STW.ProcessingApi.Function.Triggers;
using TestDoubles;

[TestClass]
public class ProcessingApiTriggerTests
{
    private Mock<ILogger<ProcessingApiTrigger>> _loggerMock;
    private Mock<IValidator> _validatorMock;
    private ProcessingApiTrigger _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger<ProcessingApiTrigger>>();
        _validatorMock = new Mock<IValidator>();
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
        _validatorMock.Setup(v => v.IsValid(It.IsAny<List<IRule>>(), It.IsAny<List<IAsyncRule>>(), "{test}"))
            .Returns(true);

        // Act
        var result = _systemUnderTest.Run(httpRequestData);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Headers.Should().ContainKey(HttpHeaders.ContentType).WhoseValue.Should().Contain(contentType);
        result.Body.Should().BeSameAs(requestBody);

        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Passed"), Times.Once);
    }

    [TestMethod]
    public void Run_LogsValidationFailure_ValidationFails()
    {
        // Arrange
        const string contentType = "application/json; charset=utf-8";
        var requestBody = new MemoryStream(Encoding.Default.GetBytes("{}"));
        var httpRequestData = new MockHttpRequestData(Mock.Of<FunctionContext>(), requestBody, HttpVerbs.Post);
        httpRequestData.Headers.Add(HttpHeaders.ContentType, contentType);
        _validatorMock.Setup(v => v.IsValid(It.IsAny<List<IRule>>(), It.IsAny<List<IAsyncRule>>(), "{}"))
            .Returns(false);

        // Act
        _systemUnderTest.Run(httpRequestData);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Failed"), Times.Once);
    }
}
