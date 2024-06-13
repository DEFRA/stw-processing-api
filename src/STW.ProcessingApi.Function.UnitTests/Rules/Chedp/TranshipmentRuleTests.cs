namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class TranshipmentRuleTests
{
    private TranshipmentRule _systemUnderTest;
    private List<ValidationError> _validationErrors;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new TranshipmentRule();
        _validationErrors = new List<ValidationError>();
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
    public void Invoke_DoesNotAddError_WhenImportSpsCountryIsPresentAndFinalBcpIsValid()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                ImportSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.NewZealandIsoCode),
                TransitSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.UkBcpCode)
                }
            },
            SpsExchangedDocument = new SpsExchangedDocument
            {
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause = new List<IncludedSpsClause>
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.Transhipment)
                        }
                    }
                },
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_AddsError_WhenPurposeIsTranshipmentButImportSpsCountryIsMissing()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                ImportSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithId(string.Empty),
                TransitSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.UkBcpCode)
                }
            },
            SpsExchangedDocument = new SpsExchangedDocument
            {
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause = new List<IncludedSpsClause>
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.Transhipment)
                        }
                    }
                },
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.MissingImportSpsCountry);
                x.ErrorId.Should().Be(RuleErrorId.MissingImportSpsCountry);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenPurposeIsTranshipmentButTransitSpsCountryHasNullId()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                ImportSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.NewZealandIsoCode),
                TransitSpsCountry = new List<SpsCountryType>
                {
                    new SpsCountryType
                    {
                        SubordinateSpsCountrySubDivision = new List<SpsCountrySubDivisionType>
                        {
                            new SpsCountrySubDivisionType()
                        }
                    }
                }
            },
            SpsExchangedDocument = new SpsExchangedDocument
            {
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause = new List<IncludedSpsClause>
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.Transhipment)
                        }
                    }
                },
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.MissingFinalBcp);
                x.ErrorId.Should().Be(RuleErrorId.MissingFinalBcp);
            });
    }
}
