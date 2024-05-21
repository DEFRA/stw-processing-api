namespace STW.ProcessingApi.Function.UnitTests.Rules.Common;

using Constants;
using FluentAssertions;
using Function.Rules.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class TotalNetWeightRuleTests
{
    private TotalNetWeightRule _systemUnderTest;
    private List<ErrorEvent> _errorEvents;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new TotalNetWeightRule();
        _errorEvents = new List<ErrorEvent>();
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
    public void Invoke_AddsError_WhenNetWeightMeasureIsLessThanMinAllowedWeight()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                NetWeightMeasure = SpsCertificateTestHelper.BuildMeasureTypeWithValue(0.0)
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.NetWeightLessThanMinWeight);
                x.ErrorId.Should().Be(RuleErrorId.NetWeightLessThanMinWeight);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenNetWeightMeasureHasMoreThanThreeDecimals()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                NetWeightMeasure = SpsCertificateTestHelper.BuildMeasureTypeWithValue(5.000001)
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.NetWeightTooManyDecimals);
                x.ErrorId.Should().Be(RuleErrorId.NetWeightTooManyDecimals);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenNetWeightMeasureHasMoreThanSixteenDigits()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                NetWeightMeasure = SpsCertificateTestHelper.BuildMeasureTypeWithValue(12345678912345678.0)
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.NetWeightTooManyDigits);
                x.ErrorId.Should().Be(RuleErrorId.NetWeightTooManyDigits);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenNetWeightMeasureIsValid()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                NetWeightMeasure = SpsCertificateTestHelper.BuildMeasureTypeWithValue(123.456)
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }
}
