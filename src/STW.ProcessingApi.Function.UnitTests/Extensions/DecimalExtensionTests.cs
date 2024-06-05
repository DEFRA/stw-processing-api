namespace STW.ProcessingApi.Function.UnitTests.Extensions;

using FluentAssertions;
using Function.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class DecimalExtensionTests
{
    [TestMethod]
    public void RemoveTrailingZeros_RemovesZeros_WhenZerosExistAtTheEndOfTheOfTheDecimal()
    {
        // Arrange
        var @decimal = new decimal(123.10000);

        // Act
        var result = @decimal.RemoveTrailingZeros();

        // Assert
        result.Should().Be(new decimal(123.1));
    }

    [TestMethod]
    public void RemoveTrailingZeros_DoesNotRemoveZeros_WhenZerosAreNotAtTheEndOfTheDecimal()
    {
        // Arrange
        var @decimal = new decimal(123.000001);

        // Act
        var result = @decimal.RemoveTrailingZeros();

        // Assert
        result.Should().Be(new decimal(123.000001));
    }

    [TestMethod]
    public void Precision_ReturnsCorrectPrecision_WhenDecimalContainsDecimalPoint()
    {
        // Arrange
        var @decimal = new decimal(1.12300);

        // Act
        var result = @decimal.Precision();

        // Assert
        result.Should().Be(4);
    }

    [TestMethod]
    public void Precision_ReturnsCorrectPrecision_WhenDecimalDoesNotContainDecimalPoint()
    {
        // Arrange
        var @decimal = new decimal(11234323);

        // Act
        var result = @decimal.Precision();

        // Assert
        result.Should().Be(8);
    }
}
