namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Function.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using TestHelpers;

[TestClass]
public class InternalMarketFieldConfigRuleTests
{
    private InternalMarketFieldConfigRule _systemUnderTest;
    private List<ValidationError> _validationErrors;
    private Mock<IFieldConfigService> _fieldConfigServiceMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _fieldConfigServiceMock = new Mock<IFieldConfigService>();
        _systemUnderTest = new InternalMarketFieldConfigRule(_fieldConfigServiceMock.Object);
        _validationErrors = new List<ValidationError>();
    }

    [TestMethod]
    [DataRow(ChedType.Chedpp, Purpose.InternalMarket)]
    [DataRow(ChedType.Chedpp, Purpose.ReEntry)]
    [DataRow(ChedType.Chedp, Purpose.DirectTransit)]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeIsNotChedpAndPurposeNotInternalMarket(
        string chedType,
        string purpose)
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
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(
                                SubjectCode.Purpose,
                                purpose)
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
    public async Task Validate_ReturnsNoErrors_WhenInternalMarketPurposeIsValid()
    {
        // Arrange
        var fieldConfig = await File.ReadAllTextAsync("TestData/fieldConfigValidInternalMarketPurpose.json");
        FieldConfigDto fieldConfigDto = new()
        {
            Id = 1,
            CommodityCode = "1234",
            CertificateType = "CHEDP",
            Data = fieldConfig
        };
        var spsCertificate = BuildSpsCertificateForGoodsCertifiedAs(GoodsCertifiedAs.HumanConsumption);
        _fieldConfigServiceMock
            .Setup(m => m.GetByCertTypeAndCommodityCode(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<FieldConfigDto>.Success(fieldConfigDto));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }

    [TestMethod]
    public async Task Validate_ReturnsErrors_WhenInternalMarketPurposeIsInvalid()
    {
        // Arrange
        var fieldConfig = await File.ReadAllTextAsync("TestData/fieldConfigValidInternalMarketPurpose.json");
        FieldConfigDto fieldConfigDto = new()
        {
            Id = 1,
            CommodityCode = "1234",
            CertificateType = "CHEDP",
            Data = fieldConfig
        };
        var spsCertificate = BuildSpsCertificateForGoodsCertifiedAs(GoodsCertifiedAs.TechnicalUse);
        _fieldConfigServiceMock
            .Setup(m => m.GetByCertTypeAndCommodityCode(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<FieldConfigDto>.Success(fieldConfigDto));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be("TECHNICAL_USE is not a valid \"GOODS_CERTIFIED_AS\" value for the first trade line item - check the value provided");
                x.ErrorId.Should().Be(94);
            });
    }

    [TestMethod]
    public async Task Validate_ReturnsErrors_WhenFieldConfigServiceHasError()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificateForGoodsCertifiedAs(GoodsCertifiedAs.TechnicalUse);
        _fieldConfigServiceMock
            .Setup(m => m.GetByCertTypeAndCommodityCode(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<FieldConfigDto>.Failure(new HttpRequestException("Message")));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be("Unable to check Field Config: Message");
                x.ErrorId.Should().Be(95);
            });
    }

    private static SpsCertificate BuildSpsCertificateForGoodsCertifiedAs(string certiedAs)
    {
        return new SpsCertificate()
        {
            SpsConsignment = new SpsConsignment
            {
                IncludedSpsConsignmentItem = new List<IncludedSpsConsignmentItem>()
                {
                    new IncludedSpsConsignmentItem()
                    {
                        IncludedSpsTradeLineItem = new List<IncludedSpsTradeLineItem>()
                        {
                            new IncludedSpsTradeLineItem()
                            {
                                ApplicableSpsClassification = new List<ApplicableSpsClassification>()
                                {
                                    new ApplicableSpsClassification()
                                    {
                                        SystemName = new List<TextType>()
                                        {
                                            new TextType()
                                            {
                                                Value = SystemName.IpaffsCct
                                            }
                                        },
                                        ClassName = new List<TextType>()
                                        {
                                            new TextType()
                                            {
                                                Value = "Farmed stock"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            SpsExchangedDocument = new SpsExchangedDocument
            {
                IncludedSpsNote = new List<SpsNoteType>()
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(
                        SubjectCode.ChedType,
                        ChedType.Chedp)
                },
                SignatorySpsAuthentication = new List<SpsAuthenticationType>()
                {
                    BuildSpsAuthenticationTypeForGoodsCertifiedAs(certiedAs)
                }
            }
        };
    }

    private static SpsAuthenticationType BuildSpsAuthenticationTypeForGoodsCertifiedAs(string certifiedAs)
    {
        return new SpsAuthenticationType()
        {
            IncludedSpsClause = new List<IncludedSpsClause>()
            {
                new IncludedSpsClause()
                {
                    Id = new IdType()
                    {
                        Value = SubjectCode.GoodsCertifiedAs
                    },
                    Content = new List<TextType>()
                    {
                        new TextType()
                        {
                            Value = certifiedAs
                        }
                    }
                }
            }
        };
    }
}
