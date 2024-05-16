using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STW.ProcessingApi.Function.Validation.Rules;

namespace STW.ProcessingApi.Function.UnitTests.Validation.Rule;

[TestClass]
public class ExampleAsyncRuleTest
{
    private ExampleAsyncRule _rule;

    [TestInitialize]
    public void TestInitialize()
    {
        _rule = new ExampleAsyncRule();
    }

    [TestMethod]
    public void Validate_ReturnsFalse_WhenInputNotValid()
    {
        // Act
        var result = _rule.ValidateAsync(null);

        // Assert
        result.Should().Be(Task.FromResult(false));
    }

    [TestMethod]
    public void Validate_ReturnsTrue_WhenInputValid()
    {
        // Act
        var result = _rule.ValidateAsync("test");

        // Assert
        result.Should().Be(Task.FromResult(true));
    }
}
