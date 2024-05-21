using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using STW.ProcessingApi.Function.Triggers;
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
    public async Task Run_ReturnsResponseWithRequestBodyAndContentType_WhenCalled()
    {
        // Arrange
        const string messageString = "Test message body";
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(BinaryData.FromString(messageString));

        _validatorMock
            .Setup(v => v.IsValidAsync(It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        await _systemUnderTest.Run(message);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Passed"), Times.Once);
        _validatorMock.Verify(x => x.IsValidAsync(messageString), Times.Once);
    }

    [TestMethod]
    public async Task Run_LogsValidationFailure_ValidationFails()
    {
        // Arrange
        const string messageString = "Test message body";
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(BinaryData.FromString(messageString));

        _validatorMock
            .Setup(v => v.IsValidAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act
        await _systemUnderTest.Run(message);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Failed"), Times.Once);
        _validatorMock.Verify(x => x.IsValidAsync(messageString), Times.Once);
    }
}
