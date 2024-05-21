namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class FreeZoneRuleTests
{
    private FreeZoneRule _systemUnderTest;
    private List<ErrorEvent> _errorEvents;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new FreeZoneRule();
        _errorEvents = new List<ErrorEvent>();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedpPurposeConformingGoodsAndDestinationTypeFreeZone()
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.NonConformingGoods)
                        }
                    }
                },
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Chedp),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.FreeZone)
                }
            }
        };

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow(ChedType.Chedpp, Purpose.NonConformingGoods, DestinationType.FreeZone)]
    [DataRow(ChedType.Chedpp, Purpose.NonConformingGoods, DestinationType.Ship)]
    [DataRow(ChedType.Chedpp, Purpose.NonConformingGoods, DestinationType.CustomsWarehouse)]
    [DataRow(ChedType.Chedpp, Purpose.Transhipment, DestinationType.FreeZone)]
    [DataRow(ChedType.Chedpp, Purpose.Transhipment, DestinationType.Ship)]
    [DataRow(ChedType.Chedpp, Purpose.Transhipment, DestinationType.CustomsWarehouse)]
    [DataRow(ChedType.Chedpp, Purpose.ReEntry, DestinationType.FreeZone)]
    [DataRow(ChedType.Chedpp, Purpose.ReEntry, DestinationType.Ship)]
    [DataRow(ChedType.Chedpp, Purpose.ReEntry, DestinationType.CustomsWarehouse)]
    [DataRow(ChedType.Chedpp, Purpose.DirectTransit, DestinationType.FreeZone)]
    [DataRow(ChedType.Chedpp, Purpose.DirectTransit, DestinationType.Ship)]
    [DataRow(ChedType.Chedpp, Purpose.DirectTransit, DestinationType.CustomsWarehouse)]
    [DataRow(ChedType.Chedpp, Purpose.InternalMarket, DestinationType.FreeZone)]
    [DataRow(ChedType.Chedpp, Purpose.InternalMarket, DestinationType.Ship)]
    [DataRow(ChedType.Chedpp, Purpose.InternalMarket, DestinationType.CustomsWarehouse)]
    [DataRow(ChedType.Chedp, Purpose.NonConformingGoods, DestinationType.Ship)]
    [DataRow(ChedType.Chedp, Purpose.NonConformingGoods, DestinationType.CustomsWarehouse)]
    [DataRow(ChedType.Chedp, Purpose.Transhipment, DestinationType.FreeZone)]
    [DataRow(ChedType.Chedp, Purpose.Transhipment, DestinationType.Ship)]
    [DataRow(ChedType.Chedp, Purpose.Transhipment, DestinationType.CustomsWarehouse)]
    [DataRow(ChedType.Chedp, Purpose.ReEntry, DestinationType.FreeZone)]
    [DataRow(ChedType.Chedp, Purpose.ReEntry, DestinationType.Ship)]
    [DataRow(ChedType.Chedp, Purpose.ReEntry, DestinationType.CustomsWarehouse)]
    [DataRow(ChedType.Chedp, Purpose.DirectTransit, DestinationType.FreeZone)]
    [DataRow(ChedType.Chedp, Purpose.DirectTransit, DestinationType.Ship)]
    [DataRow(ChedType.Chedp, Purpose.DirectTransit, DestinationType.CustomsWarehouse)]
    [DataRow(ChedType.Chedp, Purpose.InternalMarket, DestinationType.FreeZone)]
    [DataRow(ChedType.Chedp, Purpose.InternalMarket, DestinationType.Ship)]
    [DataRow(ChedType.Chedp, Purpose.InternalMarket, DestinationType.CustomsWarehouse)]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeNotChedpPurposeNotConformingGoodsAndDestinationTypeNotFreeZone(string chedType, string purpose, string destinationType)
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, purpose)
                        }
                    }
                },
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, chedType),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, destinationType)
                }
            }
        };

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenConsignmentDoesNotConformToEuAndRegisteredNumberIsPresent()
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.NonConformingGoods)
                        }
                    }
                },
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.False),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.FreeZone),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(
                        SubjectCode.NonConformingGoodsDestinationRegisteredNumber,
                        TestConstants.DestinationRegisteredNumber)
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_AddsError_WhenConsignmentConformsToEu()
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.NonConformingGoods)
                        }
                    }
                },
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.FreeZone),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.True),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(
                        SubjectCode.NonConformingGoodsDestinationRegisteredNumber,
                        TestConstants.DestinationRegisteredNumber)
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.NonConformingGoodsCannotConformToEu));
    }

    [TestMethod]
    public void Invoke_AddsError_WhenNonConformingGoodsDestinationRegisteredNumberIsMissing()
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.NonConformingGoods)
                        }
                    }
                },
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.False),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.FreeZone)
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.RegisteredNumberMissingFreeZoneNumber));
    }
}
