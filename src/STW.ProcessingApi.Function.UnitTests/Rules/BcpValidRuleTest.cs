namespace STW.ProcessingApi.Function.UnitTests.Rules;

using FluentAssertions;
using Function.Rules;
using Function.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Newtonsoft.Json;

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
        var errors = new List<ValidationError>();

        _bcpServiceMock.Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP")).ReturnsAsync(
        [
            ValidBcp,
        ]);

        // Act
        await _rule.Invoke(spsCertificate!, errors);

        // Assert
        errors.Should().BeEmpty();
    }

    [TestMethod]
    public async Task Validate_ReturnsError_WhenBcpInvalid()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(spsCertificateString);
        var errors = new List<ValidationError>();

        _bcpServiceMock.Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP")).ReturnsAsync([]);

        // Act
        await _rule.Invoke(spsCertificate!, errors);

        // Assert
        errors.Should().Equal(new ValidationError("Invalid BCP with code BCPCODE for CHED type CHEDP"));
    }

    [TestMethod]
    public async Task Validate_ReturnsError_WhenBcpSuspended()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(spsCertificateString);
        var errors = new List<ValidationError>();

        _bcpServiceMock.Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP")).ReturnsAsync(
        [
            SuspendedBcp,
        ]);

        // Act
        await _rule.Invoke(spsCertificate!, errors);

        // Assert
        errors.Should().Equal(new ValidationError("BCP with code BCPCODE for CHED type CHEDP is suspended"));
    }
}
