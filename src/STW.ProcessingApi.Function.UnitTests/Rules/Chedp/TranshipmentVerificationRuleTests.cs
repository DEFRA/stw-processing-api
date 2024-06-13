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
public class TranshipmentVerificationRuleTests
{
    private IList<ValidationError> _validationErrors;
    private Mock<ICountriesService> _countriesServiceMock;
    private Mock<IBcpService> _bcpServiceMock;
    private TranshipmentVerificationRule _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _validationErrors = new List<ValidationError>();
        _countriesServiceMock = new Mock<ICountriesService>();
        _bcpServiceMock = new Mock<IBcpService>();
        _systemUnderTest = new TranshipmentVerificationRule(_countriesServiceMock.Object, _bcpServiceMock.Object);
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedpAndPurposeIsTranshipment()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Chedp)
                },
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause = new List<IncludedSpsClause>
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.Transhipment)
                        }
                    }
                }
            }
        };

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow(ChedType.Cheda, Purpose.Transhipment)]
    [DataRow(ChedType.Cheda, Purpose.NonConformingGoods)]
    [DataRow(ChedType.Cheda, Purpose.InternalMarket)]
    [DataRow(ChedType.Cheda, Purpose.ReEntry)]
    [DataRow(ChedType.Cheda, Purpose.DirectTransit)]
    [DataRow(ChedType.Chedd, Purpose.Transhipment)]
    [DataRow(ChedType.Chedd, Purpose.NonConformingGoods)]
    [DataRow(ChedType.Chedd, Purpose.InternalMarket)]
    [DataRow(ChedType.Chedd, Purpose.ReEntry)]
    [DataRow(ChedType.Chedd, Purpose.DirectTransit)]
    [DataRow(ChedType.Chedpp, Purpose.Transhipment)]
    [DataRow(ChedType.Chedpp, Purpose.NonConformingGoods)]
    [DataRow(ChedType.Chedpp, Purpose.InternalMarket)]
    [DataRow(ChedType.Chedpp, Purpose.ReEntry)]
    [DataRow(ChedType.Chedpp, Purpose.DirectTransit)]
    [DataRow(ChedType.Chedp, Purpose.NonConformingGoods)]
    [DataRow(ChedType.Chedp, Purpose.InternalMarket)]
    [DataRow(ChedType.Chedp, Purpose.ReEntry)]
    [DataRow(ChedType.Chedp, Purpose.DirectTransit)]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeIsNotChedpAndPurposeIsNotTranshipment(string chedType, string purpose)
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, chedType)
                },
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause = new List<IncludedSpsClause>
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, purpose)
                        }
                    }
                }
            }
        };

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public async Task InvokeAsync_DoesNotAddError_WhenThirdCountryAndBcpAreBothValidAndNotSuspended()
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

        _countriesServiceMock.Verify(x => x.GetCountryOrRegionByIsoCode(TestConstants.NewZealandIsoCode), Times.Once);
        _bcpServiceMock.Verify(x => x.GetBcpsWithCodeAndType(TestConstants.UkBcpCode, "CHED-P"), Times.Once);
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenThirdCountryReturnsFailureWithInternalServerError()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _countriesServiceMock
            .Setup(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Failure(HttpStatusCode.InternalServerError));

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
            .Setup(x => x.GetCountryOrRegionByIsoCode(It.IsAny<string>()))
            .ReturnsAsync(Result<Country>.Failure(HttpStatusCode.NotFound));

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
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.UkBcpCode)
                }
            }
        };
    }
}
