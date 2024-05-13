using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STW.ProcessingApi.Function.Validation.Rule;

namespace STW.ProcessingApi.Function.UnitTests.Validation.Rule;

[TestClass]
public class ExampleRuleTest
{
    private ExampleRule _rule;

    [TestInitialize]
    public void TestInitialize()
    {
        _rule = new ExampleRule();
    }

    [TestMethod]
    public void Validate_ReturnsFalse_WhenInputNotValid()
    {
        // Act
        var result = _rule.Validate(null);

        // Assert
        result.Should().Be(false);
    }

    [TestMethod]
    public void Validate_ReturnsTrue_WhenInputValid()
    {
        // Act
        var result = _rule.Validate("test");

        // Assert
        result.Should().Be(true);
    }
}
