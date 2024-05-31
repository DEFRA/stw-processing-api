using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STW.ProcessingApi.Function.Models;
using STW.ProcessingApi.Function.Validation.Rules;

namespace STW.ProcessingApi.Function.UnitTests.Validation.Rules;

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
    public async Task Validate_ReturnsEmptyList()
    {
        // Act
        var result = await _rule.Validate(new SpsCertificate());

        // Assert
        result.Should().BeEmpty();
    }
}
