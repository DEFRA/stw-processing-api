namespace STW.ProcessingApi.Function.UnitTests.Services;

using Function.Rules.Interfaces;
using Function.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

[TestClass]
public class ValidationServiceTests
{
    private ValidationService _systemUnderTest;
    private Mock<IRule> _ruleMock;
    private Mock<IAsyncRule> _asyncRuleMock;
    private Mock<ILogger<ValidationService>> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _ruleMock = new Mock<IRule>();
        _asyncRuleMock = new Mock<IAsyncRule>();
        _loggerMock = new Mock<ILogger<ValidationService>>();
        _systemUnderTest = new ValidationService([_ruleMock.Object], [_asyncRuleMock.Object], _loggerMock.Object);
    }

    [TestMethod]
    public async Task InvokeRulesAsync_InvokesRules_WhenShouldInvokeReturnsTrue()
    {
        // Arrange
        var spsCertificate = new SpsCertificate();

        _ruleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(true);
        _asyncRuleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(true);

        // Act
        await _systemUnderTest.InvokeRulesAsync(spsCertificate);

        // Assert
        _ruleMock.Verify(x => x.Invoke(spsCertificate, It.IsAny<List<ErrorEvent>>()), Times.Once);
        _asyncRuleMock.Verify(x => x.InvokeAsync(spsCertificate, It.IsAny<List<ErrorEvent>>()), Times.Once);
    }

    [TestMethod]
    public async Task InvokeRulesAsync_DoesNotInvokeRules_WhenShouldInvokeReturnsFalse()
    {
        // Arrange
        var spsCertificate = new SpsCertificate();

        _ruleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(false);
        _asyncRuleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(false);

        // Act
        await _systemUnderTest.InvokeRulesAsync(spsCertificate);

        // Assert
        _ruleMock.Verify(x => x.Invoke(spsCertificate, It.IsAny<List<ErrorEvent>>()), Times.Never);
        _asyncRuleMock.Verify(x => x.InvokeAsync(spsCertificate, It.IsAny<List<ErrorEvent>>()), Times.Never);
    }

    [TestMethod]
    public async Task InvokeRulesAsync_LogsErrorFromRules_WhenErrorsExist()
    {
        // Arrange
        var spsCertificate = new SpsCertificate();

        _ruleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(true);
        _asyncRuleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(true);

        const string asyncRuleErrorMessage = "Error message from async rule";
        const string ruleErrorMessage = "Error message from rule";

        _ruleMock.Setup(x => x.Invoke(It.IsAny<SpsCertificate>(), It.IsAny<IList<ErrorEvent>>()))
            .Callback<SpsCertificate, IList<ErrorEvent>>((_, errorEvents) => errorEvents.Add(new ErrorEvent(ruleErrorMessage)));

        _asyncRuleMock.Setup(x => x.InvokeAsync(It.IsAny<SpsCertificate>(), It.IsAny<IList<ErrorEvent>>()))
            .Callback<SpsCertificate, IList<ErrorEvent>>((_, errorEvents) => errorEvents.Add(new ErrorEvent(asyncRuleErrorMessage)));

        // Act
        await _systemUnderTest.InvokeRulesAsync(spsCertificate);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation(ruleErrorMessage));
        _loggerMock.VerifyLog(x => x.LogInformation(asyncRuleErrorMessage));
    }
}
