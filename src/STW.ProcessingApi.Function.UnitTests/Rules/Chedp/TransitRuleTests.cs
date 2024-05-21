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
    private List<ValidationError> _validationErrors;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new TransitRule();
        _validationErrors = new List<ValidationError>();
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
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.UkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.NonUkBcpCode)
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
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
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
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.UkBcpCode)
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
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.MissingExitBcp);
                x.ErrorId.Should().Be(RuleErrorId.MissingExitBcp);
            });
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
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.UkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.NonUkBcpCode)
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
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.DuplicateTransitingCountries);
                x.ErrorId.Should().Be(RuleErrorId.DuplicateTransitingCountries);
            });
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
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.GreatBritainIsoCode, TestConstants.UkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AfghanistanIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AlbaniaIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AlgeriaIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AndorraIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AngolaIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.ArgentinaIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.ArmeniaIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AustraliaIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AustriaIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.AzerbaijanIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.BahamasIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.BahrainIsoCode, TestConstants.NonUkBcpCode),
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(TestConstants.BangladeshIsoCode, TestConstants.NonUkBcpCode)
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
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.TransitingCountriesMax, 12));
                x.ErrorId.Should().Be(RuleErrorId.TransitingCountriesMax);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenPurposeIsDirectTransitAndThirdCountryIdValueIsAnEmptyString()
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.DirectTransit)
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
                x.ErrorMessage.Should().Be(RuleErrorMessage.ThirdCountryMissing);
                x.ErrorId.Should().Be(RuleErrorId.ThirdCountryMissing);
            });
    }
}
