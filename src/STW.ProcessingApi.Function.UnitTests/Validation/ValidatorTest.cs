using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using STW.ProcessingApi.Function.Models;
using STW.ProcessingApi.Function.Validation;
using STW.ProcessingApi.Function.Validation.Rules;

namespace STW.ProcessingApi.Function.UnitTests.Validation;

[TestClass]
public class ValidatorTest
{
    private Mock<IRule> _ruleMock;
    private Mock<IAsyncRule> _asyncRuleMock;
    private List<IRule> _rulesMocksList;
    private List<IAsyncRule> _asyncRuleMocksList;
    private Validator _validator;

    [TestInitialize]
    public void TestInitialize()
    {
        _ruleMock = new Mock<IRule>();
        _asyncRuleMock = new Mock<IAsyncRule>();
        _rulesMocksList = new List<IRule> { _ruleMock.Object };
        _asyncRuleMocksList = new List<IAsyncRule> { _asyncRuleMock.Object };
        _validator = new Validator(_rulesMocksList, _asyncRuleMocksList);
    }

    [TestMethod]
    public async Task IsValid_ReturnsFalse_WhenRulesReturnFalse()
    {
        // Arrange
        var spsCertificate = new SpsCertificate();
        _ruleMock.Setup(r => r.Validate(spsCertificate)).Returns([]);
        _asyncRuleMock.Setup(r => r.ValidateAsync(spsCertificate)).ReturnsAsync([]);

        // Act
        var result = await _validator.IsValid(spsCertificate);

        // Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public async Task IsValid_ReturnsTrue_WhenRulesReturnTrue()
    {
        // Arrange
        var spsCertificate = new SpsCertificate();
        _ruleMock.Setup(r => r.Validate(spsCertificate)).Returns([new ValidationError("Message")]);
        _asyncRuleMock.Setup(r => r.ValidateAsync(spsCertificate)).ReturnsAsync([new ValidationError("Async message")]);

        // Act
        var result = await _validator.IsValid(spsCertificate);

        // Assert
        result.Should().Equal(new ValidationError("Message"), new ValidationError("Async message"));
    }
}
