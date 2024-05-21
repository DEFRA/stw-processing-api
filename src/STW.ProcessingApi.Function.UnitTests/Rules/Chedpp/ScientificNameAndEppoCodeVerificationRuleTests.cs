namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedpp;

using AutoFixture;
using AutoFixture.AutoMoq;
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
public class ScientificNameAndEppoCodeVerificationRuleTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private IList<ValidationError> _validationErrors;
    private Mock<ICommodityCodeService> _commodityCodeServiceMock;
    private ScientificNameAndEppoCodeVerificationRule _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _validationErrors = new List<ValidationError>();
        _commodityCodeServiceMock = new Mock<ICommodityCodeService>();
        _systemUnderTest = new ScientificNameAndEppoCodeVerificationRule(_commodityCodeServiceMock.Object);
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
    public async Task InvokeAsync_DoesNotAddError_WhenGetCommodityInfoByEppoCodeReturnsSuccessWithAtLeastOneElement()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ScientificName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.ScientificName)
                },
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Cn),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.CommodityId)
                    },
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Eppo),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.EppoCode)
                    }
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);
        var commodityInfo = _fixture.Create<CommodityInfo>();

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityInfoByEppoCode(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityInfo>.Success(commodityInfo));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();

        _commodityCodeServiceMock.Verify(x => x.GetCommodityInfoByEppoCode(TestConstants.CommodityId, TestConstants.EppoCode), Times.Once);
    }

    [TestMethod]
    public async Task InvokeAsync_DoesNotAddError_WhenGetCommodityInfoBySpeciesNameReturnsSuccessWithAtLeastOneElement()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ScientificName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.ScientificName)
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
        var commodityInfo = _fixture.Create<CommodityInfo>();

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityInfoBySpeciesName(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityInfo>.Success(commodityInfo));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();

        _commodityCodeServiceMock.Verify(x => x.GetCommodityInfoBySpeciesName(TestConstants.CommodityId, TestConstants.ScientificName), Times.Once);
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenGetCommodityInfoByEppoCodeReturnsSuccessWithNoElements()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ScientificName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.ScientificName)
                },
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Cn),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.CommodityId)
                    },
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Eppo),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.EppoCode)
                    }
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityInfoByEppoCode(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityInfo>.Success(new CommodityInfo()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.InvalidEppoCode, TestConstants.EppoCode, TestConstants.CommodityId));
                x.ErrorId.Should().Be(RuleErrorId.InvalidEppoCode);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenGetCommodityInfoBySpeciesNameReturnsSuccessWithNoElements()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ScientificName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.ScientificName)
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
            .Setup(x => x.GetCommodityInfoBySpeciesName(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityInfo>.Success(new CommodityInfo()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.InvalidScientificName, TestConstants.ScientificName, TestConstants.CommodityId));
                x.ErrorId.Should().Be(RuleErrorId.InvalidScientificName);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenGetCommodityInfoByEppoCodeReturnsFailure()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ScientificName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.ScientificName)
                },
                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                {
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Cn),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.CommodityId)
                    },
                    new ApplicableSpsClassification
                    {
                        SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Eppo),
                        ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.EppoCode)
                    }
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityInfoByEppoCode(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityInfo>.Failure);

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
    public async Task InvokeAsync_AddsError_WhenGetCommodityInfoBySpeciesNameReturnsFailure()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ScientificName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.ScientificName)
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
            .Setup(x => x.GetCommodityInfoBySpeciesName(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityInfo>.Failure);

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
}
