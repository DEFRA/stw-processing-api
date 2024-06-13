namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using System.Net;
using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Function.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Bcp;
using Models.Country;
using Moq;
using TestHelpers;

[TestClass]
public class TransitVerificationRuleTests
{
    private IList<ValidationError> _validationErrors;
    private Mock<ICountriesService> _countriesServiceMock;
    private Mock<IBcpService> _bcpServiceMock;
    private TransitVerificationRule _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _validationErrors = new List<ValidationError>();
        _countriesServiceMock = new Mock<ICountriesService>();
        _bcpServiceMock = new Mock<IBcpService>();
        _systemUnderTest = new TransitVerificationRule(_countriesServiceMock.Object, _bcpServiceMock.Object);
    }

    [TestMethod]
    public async Task InvokeAsync_DoesNotAddError_WhenCountriesAndBcpCodesAreValidAndNotSuspended()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _countriesServiceMock
            .Setup(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Success(new Country()));

        _bcpServiceMock
            .Setup(x => x.GetBcpsWithCodeAndType(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<List<Bcp>>.Success([new Bcp { Suspended = false }]));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();

        _countriesServiceMock.Verify(x => x.GetCountryOrRegionByIsoCode(TestConstants.AfghanistanIsoCode), Times.Once);
        _countriesServiceMock.Verify(x => x.GetCountryOrRegionByIsoCode(TestConstants.AlbaniaIsoCode), Times.Once);
        _bcpServiceMock.Verify(x => x.GetBcpsWithCodeAndType(TestConstants.UkBcpCode, "CHED-P"), Times.Once);
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenThirdCountryReturnsFailureWithInternalServerError()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _countriesServiceMock
            .SetupSequence(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Failure(HttpStatusCode.InternalServerError))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Success(new Country()));

        _bcpServiceMock
            .Setup(x => x.GetBcpsWithCodeAndType(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<List<Bcp>>.Success([new Bcp { Suspended = false }]));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.CountriesClientError);
                x.ErrorId.Should().Be(RuleErrorId.CountriesClientError);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenThirdCountryReturnsFailureWithNotFound()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _countriesServiceMock
            .SetupSequence(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Failure(HttpStatusCode.NotFound))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Success(new Country()));

        _bcpServiceMock
            .Setup(x => x.GetBcpsWithCodeAndType(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<List<Bcp>>.Success([new Bcp { Suspended = false }]));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.ThirdCountryNotFound);
                x.ErrorId.Should().Be(RuleErrorId.ThirdCountryNotFound);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenTransitingCountryReturnsFailureWithInternalServerError()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _countriesServiceMock
            .SetupSequence(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Failure(HttpStatusCode.InternalServerError))
            .ReturnsAsync(Result<Country>.Success(new Country()));

        _bcpServiceMock
            .Setup(x => x.GetBcpsWithCodeAndType(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<List<Bcp>>.Success([new Bcp { Suspended = false }]));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.CountriesClientError);
                x.ErrorId.Should().Be(RuleErrorId.CountriesClientError);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenTransitingCountryReturnsFailureWithNotFound()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _countriesServiceMock
            .SetupSequence(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Failure(HttpStatusCode.NotFound))
            .ReturnsAsync(Result<Country>.Success(new Country()));

        _bcpServiceMock
            .Setup(x => x.GetBcpsWithCodeAndType(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<List<Bcp>>.Success([new Bcp { Suspended = false }]));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.TransitingCountryNotFound, TestConstants.AfghanistanIsoCode));
                x.ErrorId.Should().Be(RuleErrorId.TransitingCountryNotFound);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_ExitBcpReturnsFailureWithInternalServerError()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _countriesServiceMock
            .SetupSequence(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Success(new Country()));

        _bcpServiceMock
            .Setup(x => x.GetBcpsWithCodeAndType(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<List<Bcp>>.Failure(HttpStatusCode.InternalServerError));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.BcpServiceError, TestConstants.UkBcpCode));
                x.ErrorId.Should().Be(RuleErrorId.BcpServiceError);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_ExitBcpReturnsSuccessWithNoElements()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _countriesServiceMock
            .SetupSequence(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Success(new Country()));

        _bcpServiceMock
            .Setup(x => x.GetBcpsWithCodeAndType(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<List<Bcp>>.Success(new List<Bcp>()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.ExitBcpNotFound);
                x.ErrorId.Should().Be(RuleErrorId.ExitBcpNotFound);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_ExitBcpReturnsSuccessWithSuspendedBcp()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _countriesServiceMock
            .SetupSequence(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Success(new Country()))
            .ReturnsAsync(Result<Country>.Success(new Country()));

        _bcpServiceMock
            .Setup(x => x.GetBcpsWithCodeAndType(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<List<Bcp>>.Success(new List<Bcp> { new Bcp { Suspended = true } }));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.BcpSuspended, TestConstants.UkBcpCode, ChedType.Chedp));
                x.ErrorId.Should().Be(RuleErrorId.BcpSuspended);
            });
    }

    private static SpsCertificate BuildSpsCertificate()
    {
        return new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Chedp)
                }
            },
            SpsConsignment = new SpsConsignment
            {
                ImportSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.NewZealandIsoCode),
                TransitSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.UkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AlbaniaIsoCode, TestConstants.NonUkBcpCode)
                }
            }
        };
    }
}
