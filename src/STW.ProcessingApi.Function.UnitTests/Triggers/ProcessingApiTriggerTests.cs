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
    public async Task Run_CallsValidationService()
    {
        // Arrange
        var spsCertificateAsBinaryData = BinaryData.FromObjectAsJson(new SpsCertificate());
        var serviceBusReceivedMessage = ServiceBusModelFactory.ServiceBusReceivedMessage(spsCertificateAsBinaryData);

        // Act
        await _systemUnderTest.Run(serviceBusReceivedMessage);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("ProcessingApiTrigger function was invoked."), Times.Once);
        _validationServiceMock.Verify(x => x.InvokeRulesAsync(It.IsAny<SpsCertificate>()), Times.Once);
    }
}
