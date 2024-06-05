namespace STW.ProcessingApi.Function.UnitTests.Rules.Common;

using Constants;
using FluentAssertions;
using Function.Rules.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class SystemIdRuleTests
{
    private SystemIdRule _systemUnderTest;
    private List<ValidationError> _validationErrors;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new SystemIdRule();
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
    public void Invoke_AddsError_WhenSystemIdIsNull()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification()
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
                x.Message.Should().Be(RuleErrorMessage.SystemIdMissing);
                x.Id.Should().Be(RuleErrorId.SystemIdMissing);
                x.TradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenSystemIdValueIsNull()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification
                    {
                        SystemId = new IdType()
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
                x.Message.Should().Be(RuleErrorMessage.SystemIdMissing);
                x.Id.Should().Be(RuleErrorId.SystemIdMissing);
                x.TradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenSystemIdValueIsNotCn()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(TestConstants.Invalid)
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
                x.Message.Should().Be(RuleErrorMessage.SystemIdMissing);
                x.Id.Should().Be(RuleErrorId.SystemIdMissing);
                x.TradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenSystemIdValueIsCn()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(TestConstants.SystemIdCn)
                    }
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }
}
