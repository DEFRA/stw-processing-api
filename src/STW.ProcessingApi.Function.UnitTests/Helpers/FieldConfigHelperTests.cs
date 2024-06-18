namespace STW.ProcessingApi.Function.UnitTests.Helpers;

using FluentAssertions;
using Function.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class FieldConfigHelperTests
{
    [TestMethod]
    public async Task GetValidPurposes_ReturnsPurposeList_WhenPurposesFoundInFieldConfig()
    {
        // Arrange
        var fieldConfig = await File.ReadAllTextAsync("TestData/fieldConfigValidInternalMarketPurpose.json");

        // Act
        var result = FieldConfigHelper.GetValidPurposes(fieldConfig, "Farmed stock");

        // Assert
        result.Should().NotBeEmpty();
    }

    [TestMethod]
    public async Task GetValidPurposes_ReturnsEmptyList_WhenNoSpeciesTypeName()
    {
        // Arrange
        var fieldConfig = await File.ReadAllTextAsync("TestData/fieldConfigValidInternalMarketPurpose.json");

        // Act
        var result = FieldConfigHelper.GetValidPurposes(fieldConfig, string.Empty);

        // Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void GetValidPurposes_ReturnsEmptyList_WhenNoPurposeInFieldConfig()
    {
        // Arrange
        var fieldConfig = "{\"NoPurpose\": \"noPurpose\"}";

        // Act
        var result = FieldConfigHelper.GetValidPurposes(fieldConfig, string.Empty);

        // Assert
        result.Should().BeEmpty();
    }
}
