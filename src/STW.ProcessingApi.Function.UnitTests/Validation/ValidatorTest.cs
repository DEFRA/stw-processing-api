namespace STW.ProcessingApi.Function.UnitTests.Validation;

using FluentAssertions;
using Function.Validation;
using Function.Validation.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

[TestClass]
public class ValidatorTest
{
    private Mock<Rule> _ruleMock;
    private Mock<AsyncRule> _asyncRuleMock;
    private List<Rule> _rulesMocksList;
    private List<AsyncRule> _asyncRuleMocksList;
    private Validator _validator;

    [TestInitialize]
    public void TestInitialize()
    {
        _ruleMock = new Mock<Rule>();
        _asyncRuleMock = new Mock<AsyncRule>();
        _rulesMocksList = new List<Rule> { _ruleMock.Object };
        _asyncRuleMocksList = new List<AsyncRule> { _asyncRuleMock.Object };
        _validator = new Validator(_rulesMocksList, _asyncRuleMocksList);
    }

    [TestMethod]
    public async Task IsValid_ReturnsFalse_WhenRulesReturnFalse()
    {
        // Arrange
        var spsCertificate = new SpsCertificate();
        _ruleMock.Setup(r => r.Validate(spsCertificate)).Returns([]);
        _asyncRuleMock.Setup(r => r.Validate(spsCertificate)).ReturnsAsync([]);

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
        _asyncRuleMock.Setup(r => r.Validate(spsCertificate)).ReturnsAsync([new ValidationError("Async message")]);

        // Act
        var result = await _validator.IsValid(spsCertificate);

        // Assert
        result.Should().Equal(new ValidationError("Message"), new ValidationError("Async message"));
    }
}
