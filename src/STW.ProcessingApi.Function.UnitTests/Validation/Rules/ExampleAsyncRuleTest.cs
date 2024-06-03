namespace STW.ProcessingApi.Function.UnitTests.Validation.Rules;

using FluentAssertions;
using Function.Validation.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

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
