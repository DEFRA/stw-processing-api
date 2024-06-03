namespace STW.ProcessingApi.Function.UnitTests.Validation.Rules;

using FluentAssertions;
using Function.Validation.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Newtonsoft.Json;

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
    public void Validate_ReturnsErrors_WhenInputInvalid()
    {
        // Arrange
        var spsCertificateString = File.ReadAllText("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(spsCertificateString);

        // Act
        var result = _rule.Validate(spsCertificate!);

        // Assert
        result.Should().Equal(new ValidationError("Trade line item is missing an EPPO code", 4, 0));
    }

    [TestMethod]
    public void Validate_ReturnsNoErrors_WhenInputValid()
    {
        // Arrange
        var spsCertificateString = File.ReadAllText("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(spsCertificateString);
        spsCertificate!.SpsConsignment.IncludedSpsConsignmentItem.First().IncludedSpsTradeLineItem.First()
            .ApplicableSpsClassification.Add(
                new ApplicableSpsClassification()
                {
                    SystemID = new IDType()
                    {
                        Value = "EPPO",
                    },
                    ClassCode = new CodeType()
                    {
                        Value = "MABAN",
                    },
                });

        // Act
        var result = _rule.Validate(spsCertificate);

        // Assert
        result.Should().BeEmpty();
    }
}
