using Azure.Core.Amqp;
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
    public void Run_ReturnsResponseWithRequestBodyAndContentType_WhenCalled()
    {
        // Arrange
        var messageBody = BinaryData.FromString("Test message body");
        var messageMock = new Mock<AmqpAnnotatedMessage>();
        var x = ServiceBusReceivedMessage.FromAmqpMessage(messageMock.Object, BinaryData.FromString(string.Empty));
        messageMock.Setup(m => m.Body).Returns(messageBody);

        _validatorMock.Setup(v => v.IsValidAsync("Test message body"))
            .Returns(Task.FromResult(true));

        // Act
        var result = _systemUnderTest.Run(messageMock.Object);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Passed"), Times.Once);
    }

    [TestMethod]
    public async Task Run_LogsValidationFailure_ValidationFails()
    {
        // Arrange
        var messageBody = BinaryData.FromString(string.Empty);
        var messageMock = new Mock<ServiceBusReceivedMessage>(
            "messageId",
            TimeSpan.FromMinutes(5),
            DateTime.UtcNow,
            messageBody);
        messageMock.SetupGet(m => m.Body).Returns(messageBody);
        _validatorMock.Setup(v => v.IsValidAsync("Test message body"))
            .Returns(Task.FromResult<bool>(false));

        // Act
        await _systemUnderTest.Run(messageMock.Object);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Validation Failed"), Times.Once);
    }
}
