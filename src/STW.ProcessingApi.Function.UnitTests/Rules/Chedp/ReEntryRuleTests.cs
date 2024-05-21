namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class ReEntryRuleTests
{
    private ReEntryRule _systemUnderTest;
    private List<ErrorEvent> _errorEvents;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new ReEntryRule();
        _errorEvents = new List<ErrorEvent>();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedpAndPurposeIsReEntry()
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.ReEntry)
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
    [DataRow(ChedType.Chedpp, Purpose.ReEntry)]
    [DataRow(ChedType.Chedpp, Purpose.NonConformingGoods)]
    [DataRow(ChedType.Chedpp, Purpose.InternalMarket)]
    [DataRow(ChedType.Chedpp, Purpose.DirectTransit)]
    [DataRow(ChedType.Chedpp, Purpose.Transhipment)]
    [DataRow(ChedType.Chedp, Purpose.NonConformingGoods)]
    [DataRow(ChedType.Chedp, Purpose.InternalMarket)]
    [DataRow(ChedType.Chedp, Purpose.DirectTransit)]
    [DataRow(ChedType.Chedp, Purpose.Transhipment)]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeIsNotChedpAndPurposeIsNotReEntry(string chedType, string purpose)
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
    public void Invoke_DoesNotAddError_WhenPurposeIsReEntryAndConformsToEuSpsNoteTypeContentValueIsTrue()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause = new List<IncludedSpsClause>
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.ReEntry)
                        }
                    }
                },
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.True)
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_DoesAddsError_WhenPurposeIsReEntryAndConformsToEuSpsNoteTypeContentValueIsFalse()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause = new List<IncludedSpsClause>
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.ReEntry)
                        }
                    }
                },
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.False)
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.ConformsToEuMustBeTrueForReEntry));
    }

    [TestMethod]
    public void Invoke_AddsError_WhenPurposeIsReEntryAndConformsToEuSpsNoteTypeIsNotProvided()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause = new List<IncludedSpsClause>
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.ReEntry)
                        }
                    }
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.ConformsToEuMustBeTrueForReEntry));
    }
}
