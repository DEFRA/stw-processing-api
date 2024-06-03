namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedpp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedpp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class QuantityTypeRuleTests
{
    private QuantityTypeRule _systemUnderTest;
    private List<ValidationError> _validationErrors;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new QuantityTypeRule();
        _validationErrors = new List<ValidationError>();
    }

    [TestMethod]
    [DataRow(ChedType.Chedp)]
    [DataRow(TestConstants.Invalid)]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeNotChedpp(string chedType)
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, chedType)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedpp()
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Chedpp)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow(TestConstants.StemsKey)]
    [DataRow(TestConstants.BulbsKey)]
    [DataRow(TestConstants.CormsAndRhizomesKey)]
    [DataRow(TestConstants.PlantsAndTissueCultureKey)]
    [DataRow(TestConstants.SeedsKey)]
    [DataRow(TestConstants.KilogramsKey)]
    [DataRow(TestConstants.PiecesKey)]
    public void Invoke_DoesNotAddError_WhenQuantityIsValid(string contentCodeValue)
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContentAndContentCode(SubjectCode.Quantity, "1", contentCodeValue)
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_AddError_WhenQuantityContentTypeIsZero()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContentAndContentCode(SubjectCode.Quantity, "0", TestConstants.StemsKey)
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.QuantityMustBeOneOrMore);
                x.ErrorId.Should().Be(RuleErrorId.QuantityMustBeOneOrMore);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddError_WhenQuantityContentTypeIsNotNumeric()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContentAndContentCode(SubjectCode.Quantity, TestConstants.Invalid, TestConstants.StemsKey)
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.InvalidQuantityType);
                x.ErrorId.Should().Be(RuleErrorId.InvalidQuantityType);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddError_WhenQuantityContentTypeIsNegative()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContentAndContentCode(SubjectCode.Quantity, "-1", TestConstants.StemsKey)
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.QuantityMustBeOneOrMore);
                x.ErrorId.Should().Be(RuleErrorId.QuantityMustBeOneOrMore);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddError_WhenQuantityContentCodeIsMissing()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    new SpsNoteType
                    {
                        SubjectCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(SubjectCode.Quantity),
                        Content =
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue("-1")
                        }
                    }
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.InvalidQuantityType);
                x.ErrorId.Should().Be(RuleErrorId.InvalidQuantityType);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddError_WhenQuantityContentIsMissing()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    new SpsNoteType
                    {
                        SubjectCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(SubjectCode.Quantity),
                        ContentCode =
                        {
                            SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.StemsKey)
                        }
                    }
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.InvalidQuantityType);
                x.ErrorId.Should().Be(RuleErrorId.InvalidQuantityType);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddError_WhenThereAreNoSpsNoteTypesWithQuantitySubjectCode()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    new SpsNoteType
                    {
                        SubjectCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(SubjectCode.ConformsToEu)
                    }
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.MissingQuantityType);
                x.ErrorId.Should().Be(RuleErrorId.MissingQuantityType);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }
}
