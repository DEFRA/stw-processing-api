using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using STW.ProcessingApi.Function.Models;
using STW.ProcessingApi.Function.Services;
using STW.ProcessingApi.Function.Validation.Rules;

namespace STW.ProcessingApi.Function.UnitTests.Validation.Rules;

[TestClass]
public class BcpValidRuleTest
{
    private static readonly Bcp ValidBcp = new()
    {
        Id = 1,
        Code = "BCPCODE",
        Name = "BCP Name",
        Suspended = false,
    };

    private static readonly Bcp SuspendedBcp = new()
    {
        Id = 1,
        Code = "BCPCODE",
        Name = "BCP Name",
        Suspended = true,
    };

    private Mock<IBcpService> _bcpServiceMock;
    private BcpValidRule _rule;

    [TestInitialize]
    public void TestInitialize()
    {
        _bcpServiceMock = new Mock<IBcpService>();
        _rule = new BcpValidRule(_bcpServiceMock.Object);
    }

    [TestMethod]
    public async Task Validate_ReturnsNoErrors_WhenBcpIsValid()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(spsCertificateString);

        _bcpServiceMock.Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP")).ReturnsAsync(
        [
            ValidBcp,
        ]);

        // Act
        var result = await _rule.Validate(spsCertificate!);

        // Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public async Task Validate_ReturnsError_WhenBcpInvalid()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(spsCertificateString);

        _bcpServiceMock.Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP")).ReturnsAsync([]);

        // Act
        var result = await _rule.Validate(spsCertificate!);

        // Assert
        result.Should().Equal(new ValidationError("Invalid BCP with code BCPCODE for CHED type CHEDP"));
    }

    [TestMethod]
    public async Task Validate_ReturnsError_WhenBcpSuspended()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(spsCertificateString);

        _bcpServiceMock.Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP")).ReturnsAsync(
        [
            SuspendedBcp,
        ]);

        // Act
        var result = await _rule.Validate(spsCertificate!);

        // Assert
        result.Should().Equal(new ValidationError("BCP with code BCPCODE for CHED type CHEDP is suspended"));
    }
}
