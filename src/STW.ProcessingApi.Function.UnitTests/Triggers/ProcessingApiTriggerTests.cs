namespace STW.ProcessingApi.Function.UnitTests.Triggers;

using Azure.Messaging.ServiceBus;
using Function.Services.Interfaces;
using Function.Triggers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

[TestClass]
public class ProcessingApiTriggerTests
{
    private Mock<ILogger<ProcessingApiTrigger>> _loggerMock;
    private Mock<IValidationService> _validationServiceMock;
    private ProcessingApiTrigger _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger<ProcessingApiTrigger>>();
        _validationServiceMock = new Mock<IValidationService>();
        _systemUnderTest = new ProcessingApiTrigger(_loggerMock.Object, _validationServiceMock.Object);
    }

    [TestMethod]
    public async Task Run_LogsSuccess_WhenNoErrors()
    {
        // Arrange
        var spsCertificateAsBinaryData = BinaryData.FromObjectAsJson(new SpsCertificate());
        var serviceBusReceivedMessage = ServiceBusModelFactory.ServiceBusReceivedMessage(spsCertificateAsBinaryData);
        _validationServiceMock.Setup(x => x.InvokeRulesAsync(It.IsAny<SpsCertificate>())).ReturnsAsync([]);

        // Act
        await _systemUnderTest.Run(serviceBusReceivedMessage);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Validation passed"), Times.Once());
        _validationServiceMock.Verify(x => x.InvokeRulesAsync(It.IsAny<SpsCertificate>()), Times.Once);
    }

    [TestMethod]
    public async Task Run_LogsErrors_WhenErrors()
    {
        // Arrange
        var spsCertificateAsBinaryData = BinaryData.FromObjectAsJson(new SpsCertificate());
        var serviceBusReceivedMessage = ServiceBusModelFactory.ServiceBusReceivedMessage(spsCertificateAsBinaryData);
        _validationServiceMock.Setup(x => x.InvokeRulesAsync(It.IsAny<SpsCertificate>()))
            .ReturnsAsync(
            [
                new ValidationError("Message 1", 1),
                new ValidationError("Message 2", 2)
            ]);

        // Act
        await _systemUnderTest.Run(serviceBusReceivedMessage);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _loggerMock.VerifyLog(x => x.LogWarning("Validation failed"), Times.Once());
        _loggerMock.VerifyLog(x => x.LogWarning("Message 1, Message 2"), Times.Once());
        _validationServiceMock.Verify(x => x.InvokeRulesAsync(It.IsAny<SpsCertificate>()), Times.Once);
    }
}
