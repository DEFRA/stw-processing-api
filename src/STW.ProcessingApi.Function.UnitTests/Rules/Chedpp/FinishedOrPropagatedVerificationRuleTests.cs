namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedpp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedpp;
using Function.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Commodity;
using Moq;
using TestHelpers;

[TestClass]
public class FinishedOrPropagatedVerificationRuleTests
{
    private Mock<ICommodityCodeService> _commodityCodeServiceMock;
    private FinishedOrPropagatedVerificationRule _systemUnderTest;
    private List<ValidationError> _validationErrors;

    [TestInitialize]
    public void TestInitialize()
    {
        _commodityCodeServiceMock = new Mock<ICommodityCodeService>();
        _systemUnderTest = new FinishedOrPropagatedVerificationRule(_commodityCodeServiceMock.Object);
        _validationErrors = new List<ValidationError>();
    }

    [TestMethod]
    [DataRow(ChedType.Chedp)]
    [DataRow(ChedType.Cheda)]
    [DataRow(ChedType.Chedd)]
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
    public async Task InvokeAsync_DoesNotAddError_WhenFinishedOrPropagatedIsRequiredAndProvided()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.FinishedOrPropagated, FinishedOrPropagated.Finished)
                },
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Cn),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.CommodityId)
                    }
                }
            }
        };

        var commodityConfigurations = new List<CommodityConfiguration>
        {
            new CommodityConfiguration
            {
                CommodityCode = TestConstants.CommodityId,
                RequiresFinishedOrPropagated = true
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityConfigurations(It.IsAny<List<string>>()))
            .ReturnsAsync(Result<IList<CommodityConfiguration>>.Success(commodityConfigurations));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }

    [TestMethod]
    public async Task InvokeAsync_DoesNotAddError_WhenFinishedOrPropagatedIsNotExpectedAndNotProvided()
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
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Cn),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.CommodityId)
                    }
                }
            }
        };

        var commodityConfigurations = new List<CommodityConfiguration>
        {
            new CommodityConfiguration
            {
                CommodityCode = TestConstants.CommodityId,
                RequiresFinishedOrPropagated = false
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityConfigurations(It.IsAny<List<string>>()))
            .ReturnsAsync(Result<IList<CommodityConfiguration>>.Success(commodityConfigurations));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenGetCommodityConfigurationsReturnsFailure()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.FinishedOrPropagated, FinishedOrPropagated.Finished)
                },
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Cn),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.CommodityId)
                    }
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityConfigurations(It.IsAny<List<string>>()))
            .ReturnsAsync(Result<IList<CommodityConfiguration>>.Failure);

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.CommodityCodeClientError);
                x.ErrorId.Should().Be(RuleErrorId.CommodityCodeClientError);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenFinishedOrPropagatedIsNotExpectedAndIsProvided()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                AdditionalInformationSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.FinishedOrPropagated, FinishedOrPropagated.Finished)
                },
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Cn),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.CommodityId)
                    }
                }
            }
        };

        var commodityConfigurations = new List<CommodityConfiguration>
        {
            new CommodityConfiguration
            {
                CommodityCode = TestConstants.CommodityId,
                RequiresFinishedOrPropagated = false
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityConfigurations(It.IsAny<List<string>>()))
            .ReturnsAsync(Result<IList<CommodityConfiguration>>.Success(commodityConfigurations));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.FinishedOrPropagatedNotRequired, TestConstants.CommodityId));
                x.ErrorId.Should().Be(RuleErrorId.FinishedOrPropagatedNotRequired);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddError_WhenFinishedOrPropagatedIsExpectedAndMissing()
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
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Cn),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.CommodityId)
                    }
                }
            }
        };

        var commodityConfigurations = new List<CommodityConfiguration>
        {
            new CommodityConfiguration
            {
                CommodityCode = TestConstants.CommodityId,
                RequiresFinishedOrPropagated = true
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityConfigurations(It.IsAny<List<string>>()))
            .ReturnsAsync(Result<IList<CommodityConfiguration>>.Success(commodityConfigurations));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.MissingFinishedOrPropagated, 1));
                x.ErrorId.Should().Be(RuleErrorId.MissingFinishedOrPropagated);
            });
    }
}
