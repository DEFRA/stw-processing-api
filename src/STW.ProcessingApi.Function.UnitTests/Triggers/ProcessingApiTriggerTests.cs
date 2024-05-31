using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using STW.ProcessingApi.Function.Models;
using STW.ProcessingApi.Function.Triggers;
using STW.ProcessingApi.Function.UnitTests.Constants;
using STW.ProcessingApi.Function.UnitTests.TestDoubles;
using STW.ProcessingApi.Function.Validation;

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
    public async Task Run_ReturnsOkResponse_WhenNoValidationErrors()
    {
        // Arrange
        var requestBody = File.OpenRead("TestData/minimalSpsCertificate.json");
        var httpRequestData = new MockHttpRequestData(Mock.Of<FunctionContext>(), requestBody, HttpVerbs.Post);
        _validatorMock.Setup(v => v.IsValid(It.IsAny<SpsCertificate>()))
            .ReturnsAsync([]);

        // Act
        var result = await _systemUnderTest.Run(httpRequestData);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Body.Should().HaveLength(0);

        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Passed"), Times.Once);
    }

    [TestMethod]
    public async Task Run_LogsValidationFailure_ValidationFails()
    {
        // Arrange
        var requestBody = File.OpenRead("TestData/minimalSpsCertificate.json");
        var httpRequestData = new MockHttpRequestData(Mock.Of<FunctionContext>(), requestBody, HttpVerbs.Post);
        _validatorMock.Setup(v => v.IsValid(It.IsAny<SpsCertificate>()))
            .ReturnsAsync([new ValidationError("Error message 1"), new ValidationError("Error message 2")]);

        // Act
        var result = await _systemUnderTest.Run(httpRequestData);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Body.Position = 0;
        var body = await new StreamReader(result.Body).ReadToEndAsync();
        body.Should().Be("Error message 1, Error message 2");

        _loggerMock.VerifyLog(x => x.LogWarning("Validation Failed"), Times.Once);
    }
}
