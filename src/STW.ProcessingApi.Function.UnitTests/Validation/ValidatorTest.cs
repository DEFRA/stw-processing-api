using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using STW.ProcessingApi.Function.Validation;
using STW.ProcessingApi.Function.Validation.Interfaces;

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
    public void IsValid_ReturnsFalse_WhenRulesReturnFalse()
    {
        // Arrange
        _ruleMock.Setup(r => r.Validate("test")).Returns(false);
        _asyncRuleMock.Setup(r => r.ValidateAsync("test"))
            .Returns(Task.FromResult(false));

        // Act
        var result = _validator.IsValid("test");

        // Assert
        result.Should().Be(false);
    }

    [TestMethod]
    public void IsValid_ReturnsTrue_WhenRulesReturnTrue()
    {
        // Arrange
        _ruleMock.Setup(r => r.Validate("test")).Returns(true);
        _asyncRuleMock.Setup(r => r.ValidateAsync("test"))
            .Returns(Task.FromResult(true));

        // Act
        var result = _validator.IsValid("test");

        // Assert
        result.Should().Be(true);
    }
}
