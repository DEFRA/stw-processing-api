namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class TransitRuleTests
{
    private TransitRule _systemUnderTest;
    private List<ErrorEvent> _errorEvents;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new TransitRule();
        _errorEvents = new List<ErrorEvent>();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedpAndPurposeIsDirectTransit()
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.DirectTransit)
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
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeIsNotChedpAndPurposeIsDirectTransit()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Chedpp)
                },
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause = new List<IncludedSpsClause>
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.DirectTransit)
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
    [DataRow(ChedType.Chedpp, Purpose.DirectTransit)]
    [DataRow(ChedType.Chedpp, Purpose.NonConformingGoods)]
    [DataRow(ChedType.Chedpp, Purpose.InternalMarket)]
    [DataRow(ChedType.Chedpp, Purpose.ReEntry)]
    [DataRow(ChedType.Chedpp, Purpose.Transhipment)]
    [DataRow(ChedType.Chedpp, Purpose.NonConformingGoods)]
    [DataRow(ChedType.Chedpp, Purpose.InternalMarket)]
    [DataRow(ChedType.Chedpp, Purpose.ReEntry)]
    [DataRow(ChedType.Chedpp, Purpose.Transhipment)]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeIsNotChedpAndPurposeIsNotDirectTransit(string chedType, string purpose)
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
    public void Invoke_DoesNotAddError_WhenPurposeIsDirectTransitAndAllRequiredFieldsArePresent()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                ImportSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.NewZealandIsoCode),
                TransitSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.BcpCode)
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.DirectTransit)
                        }
                    }
                },
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_AddsError_WhenPurposeIsDirectTransitAndExitBcpIsMissing()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                ImportSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.NewZealandIsoCode),
                TransitSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.BcpCode)
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.DirectTransit)
                        }
                    }
                },
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.MissingExitBcp));
    }

    [TestMethod]
    public void Invoke_AddsError_WhenPurposeIsDirectTransitAndThereAreDuplicateTransitingCountries()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                ImportSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.NewZealandIsoCode),
                TransitSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.BcpCode)
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.DirectTransit)
                        }
                    }
                },
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.DuplicateTransitingCountries));
    }

    [TestMethod]
    public void Invoke_AddsError_WhenPurposeIsDirectTransitAndThereAreMoreThanTwelveTransitingCountries()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                ImportSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.NewZealandIsoCode),
                TransitSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AlbaniaIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AlgeriaIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AndorraIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AngolaIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.ArgentinaIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.ArmeniaIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AustraliaIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AustriaIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AzerbaijanIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.BahamasIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.BahrainIsoCode, TestConstants.BcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.BangladeshIsoCode, TestConstants.BcpCode)
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.DirectTransit)
                        }
                    }
                },
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.TransitingCountriesMax, 12)));
    }
}
