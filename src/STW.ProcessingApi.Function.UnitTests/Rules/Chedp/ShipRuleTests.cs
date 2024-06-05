namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class ShipRuleTests
{
    private ShipRule _systemUnderTest;
    private List<ValidationError> _validationErrors;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new ShipRule();
        _validationErrors = new List<ValidationError>();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedpPurposeConformingGoodsAndDestinationTypeShip()
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
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.Ship)
                }
            }
        };

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow(ChedType.Chedpp, Purpose.NonConformingGoods, DestinationType.Ship)]
    [DataRow(ChedType.Chedpp, Purpose.NonConformingGoods, DestinationType.FreeZone)]
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
    [DataRow(ChedType.Chedp, Purpose.NonConformingGoods, DestinationType.FreeZone)]
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
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeNotChedpPurposeNotConformingGoodsAndDestinationTypeNotShip(string chedType, string purpose, string destinationType)
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
    public void Invoke_DoesNotAddError_WhenConsignmentDoesNotConformToEuAndShipPortAndNameArePresent()
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
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.Ship),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationShipName, TestConstants.ShipName),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationShipPort, TestConstants.ShipPort)
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
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
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.Ship),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.True),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationShipName, TestConstants.ShipName),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationShipPort, TestConstants.ShipPort)
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.NonConformingGoodsCannotConformToEu);
                x.Id.Should().Be(RuleErrorId.NonConformingGoodsCannotConformToEu);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenNonConformingGoodsDestinationShipNameIsMissing()
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
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.Ship),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationShipPort, TestConstants.ShipPort)
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.NonConformingGoodsShipNameMissing);
                x.Id.Should().Be(RuleErrorId.NonConformingGoodsShipNameMissing);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenNonConformingGoodsDestinationShipPortIsMissing()
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
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.Ship),
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationShipName, TestConstants.ShipName)
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.NonConformingGoodsShipPortMissing);
                x.Id.Should().Be(RuleErrorId.NonConformingGoodsShipPortMissing);
            });
    }
}
