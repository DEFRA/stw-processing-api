namespace STW.ProcessingApi.Function.UnitTests.Rules.Common;

using Constants;
using FluentAssertions;
using Function.Rules.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class TradeLineItemSequenceRuleTests
{
    private TradeLineItemSequenceRule _systemUnderTest;
    private List<ValidationError> _validationErrors;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new TradeLineItemSequenceRule();
        _validationErrors = new List<ValidationError>();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeNotChedppOrChedp()
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, TestConstants.Invalid)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    [DataRow(ChedType.Chedpp)]
    [DataRow(ChedType.Chedp)]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedppOrChedp(string chedType)
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
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Invoke_AddsError_WhenTradeLineItemSequenceNumericValuesAreNotInOrder()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(2)
            },
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1)
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(2).And.SatisfyRespectively(
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.SequenceNumericOrder);
                x.Id.Should().Be(RuleErrorId.SequenceNumericOrder);
                x.TradeLineItem.Should().Be(2);
            },
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.SequenceNumericOrder);
                x.Id.Should().Be(RuleErrorId.SequenceNumericOrder);
                x.TradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenTradeLineItemsHaveDuplicateSequenceNumericValues()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1)
            },
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1)
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.SequenceNumericOrder);
                x.Id.Should().Be(RuleErrorId.SequenceNumericOrder);
                x.TradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenTradeLineItemSequenceNumericValuesAreInOrder()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1)
            },
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(2)
            },
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(3)
            },
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(4)
            },
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }
}
