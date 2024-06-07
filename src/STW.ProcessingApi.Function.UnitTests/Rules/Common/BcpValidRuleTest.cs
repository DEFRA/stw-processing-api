namespace STW.ProcessingApi.Function.UnitTests.Rules.Common;

using System.Text.Json;
using Constants;
using FluentAssertions;
using Function.Rules.Common;
using Function.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using TestHelpers;

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
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeNotNull()
    {
        // Arrange
        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(
        [
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Cheda)
        ]);

        // Act
        var result = _rule.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeNull()
    {
        // Arrange
        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes([]);

        // Act
        var result = _rule.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public async Task Validate_ReturnsNoErrors_WhenBcpIsValid()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonSerializer.Deserialize<SpsCertificate>(spsCertificateString);
        var errors = new List<ValidationError>();

        _bcpServiceMock
            .Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP"))
            .ReturnsAsync(Result<List<Bcp>>.Success([ValidBcp]));

        // Act
        await _rule.InvokeAsync(spsCertificate!, errors);

        // Assert
        errors.Should().BeEmpty();
    }

    [TestMethod]
    public async Task Validate_ReturnsError_WhenBcpInvalid()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonSerializer.Deserialize<SpsCertificate>(spsCertificateString);
        var errors = new List<ValidationError>();

        _bcpServiceMock
            .Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP"))
            .ReturnsAsync(Result<List<Bcp>>.Success([]));

        // Act
        await _rule.InvokeAsync(spsCertificate!, errors);

        // Assert
        errors.Should().SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be("Invalid BCP with code BCPCODE for CHED type CHEDP");
                x.ErrorId.Should().Be(91);
            });
    }

    [TestMethod]
    public async Task Validate_ReturnsError_WhenBcpSuspended()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonSerializer.Deserialize<SpsCertificate>(spsCertificateString);
        var errors = new List<ValidationError>();

        _bcpServiceMock
            .Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP"))
            .ReturnsAsync(Result<List<Bcp>>.Success([SuspendedBcp]));

        // Act
        await _rule.InvokeAsync(spsCertificate!, errors);

        // Assert
        errors.Should().SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be("BCP with code BCPCODE for CHED type CHEDP is suspended");
                x.ErrorId.Should().Be(92);
            });
    }

    [TestMethod]
    public async Task Validate_ReturnsError_WhenBcpServiceHasError()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonSerializer.Deserialize<SpsCertificate>(spsCertificateString);
        var errors = new List<ValidationError>();

        _bcpServiceMock
            .Setup(m => m.GetBcpsWithCodeAndType("BCPCODE", "CHEDP"))
            .ReturnsAsync(Result<List<Bcp>>.Failure(new HttpRequestException("Message")));

        // Act
        await _rule.InvokeAsync(spsCertificate!, errors);

        // Assert
        errors.Should().SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be("Unable to check BCP validity: Message");
                x.ErrorId.Should().Be(93);
            });
    }
}
