namespace STW.ProcessingApi.Function.UnitTests.Services;

using FluentAssertions;
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
        var result = await _systemUnderTest.InvokeRulesAsync(spsCertificate);

        // Assert
        _ruleMock.Verify(x => x.Invoke(spsCertificate, It.IsAny<List<ValidationError>>()), Times.Once);
        _asyncRuleMock.Verify(x => x.InvokeAsync(spsCertificate, It.IsAny<List<ValidationError>>()), Times.Once);
        result.Should().BeEmpty();
    }

    [TestMethod]
    public async Task InvokeRulesAsync_DoesNotInvokeRules_WhenShouldInvokeReturnsFalse()
    {
        // Arrange
        var spsCertificate = new SpsCertificate();

        _ruleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(false);
        _asyncRuleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(false);

        // Act
        var result = await _systemUnderTest.InvokeRulesAsync(spsCertificate);

        // Assert
        _ruleMock.Verify(x => x.Invoke(spsCertificate, It.IsAny<List<ValidationError>>()), Times.Never);
        _asyncRuleMock.Verify(x => x.InvokeAsync(spsCertificate, It.IsAny<List<ValidationError>>()), Times.Never);
        result.Should().BeEmpty();
    }

    [TestMethod]
    public async Task InvokeRulesAsync_ReturnsErrorFromRules_WhenErrorsExist()
    {
        // Arrange
        var spsCertificate = new SpsCertificate();

        _ruleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(true);
        _asyncRuleMock.Setup(x => x.ShouldInvoke(It.IsAny<SpsCertificate>())).Returns(true);

        const string asyncRuleErrorMessage = "Error message from async rule";
        const int asyncRuleErrorId = 1;
        const string ruleErrorMessage = "Error message from rule";
        const int ruleErrorId = 2;

        _ruleMock.Setup(x => x.Invoke(It.IsAny<SpsCertificate>(), It.IsAny<IList<ValidationError>>()))
            .Callback<SpsCertificate, IList<ValidationError>>((_, validationErrors) => validationErrors.Add(new ValidationError(ruleErrorMessage, ruleErrorId)));

        _asyncRuleMock.Setup(x => x.InvokeAsync(It.IsAny<SpsCertificate>(), It.IsAny<IList<ValidationError>>()))
            .Callback<SpsCertificate, IList<ValidationError>>((_, validationErrors) => validationErrors.Add(new ValidationError(asyncRuleErrorMessage, asyncRuleErrorId)));

        // Act
        var result = await _systemUnderTest.InvokeRulesAsync(spsCertificate);

        // Assert
        result.Should().SatisfyRespectively(
            first =>
            {
                first.ErrorMessage.Should().Be(ruleErrorMessage);
                first.ErrorId.Should().Be(ruleErrorId);
            },
            second =>
            {
                second.ErrorMessage.Should().Be(asyncRuleErrorMessage);
                second.ErrorId.Should().Be(asyncRuleErrorId);
            });
    }
}
