namespace STW.ProcessingApi.Function.UnitTests.Triggers;

using Azure.Messaging.ServiceBus;
using Function.Triggers;
using Function.Validation;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

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
    public async Task Run_LogsNoErrors_WhenNoValidationErrors()
    {
        // Arrange
        var messageStream = File.OpenRead("TestData/minimalSpsCertificate.json");
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(await BinaryData.FromStreamAsync(messageStream));
        _validatorMock
            .Setup(v => v.IsValid(It.IsAny<SpsCertificate>()))
            .ReturnsAsync([]);

        // Act
        await _systemUnderTest.Run(message);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Passed"), Times.Once);
    }

    [TestMethod]
    public async Task Run_LogsValidationFailure_WhenValidationErrors()
    {
        // Arrange
        var messageStream = File.OpenRead("TestData/minimalSpsCertificate.json");
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(BinaryData.FromStream(messageStream));
        _validatorMock
            .Setup(v => v.IsValid(It.IsAny<SpsCertificate>()))
            .ReturnsAsync([new ValidationError("Error message 1"), new ValidationError("Error message 2")]);

        // Act
        await _systemUnderTest.Run(message);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogWarning("Validation Failed"), Times.Once);
        _loggerMock.VerifyLog(x => x.LogWarning("Error message 1, Error message 2"), Times.Once);
    }
}
