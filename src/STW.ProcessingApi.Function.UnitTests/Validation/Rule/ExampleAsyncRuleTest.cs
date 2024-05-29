using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STW.ProcessingApi.Function.Models;
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
    public async Task ValidateAsync_ReturnsEmptyList()
    {
        // Act
        var result = await _rule.ValidateAsync(new SpsCertificate());

        // Assert
        result.Should().BeEmpty();
    }
}
