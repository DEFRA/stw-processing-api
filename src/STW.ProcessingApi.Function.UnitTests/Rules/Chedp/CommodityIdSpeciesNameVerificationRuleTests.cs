namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using System.Text.Json;
using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Function.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Commodity;
using Moq;
using TestHelpers;

[TestClass]
public class CommodityIdSpeciesNameVerificationRuleTests
{
    private IList<ValidationError> _validationErrors;
    private Mock<ICommodityCodeService> _commodityCodeServiceMock;
    private CommodityIdSpeciesNameVerificationRule _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _validationErrors = new List<ValidationError>();
        _commodityCodeServiceMock = new Mock<ICommodityCodeService>();
        _systemUnderTest = new CommodityIdSpeciesNameVerificationRule(_commodityCodeServiceMock.Object);
    }

    [TestMethod]
    [DataRow(ChedType.Chedpp)]
    [DataRow(ChedType.Cheda)]
    [DataRow(ChedType.Chedd)]
    [DataRow(TestConstants.Invalid)]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeNotChedp(string chedType)
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
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedp()
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Chedp)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_GetCommodityCategoriesReturnsFailure()
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

        var spsCertificate = BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityCategories(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityCategory>.Failure);

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
    public async Task InvokeAsync_AddsError_WhenCommodityCategoryDoesNotHaveTypeInfo()
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

        var spsCertificate = BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityCategories(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityCategory>.Success(new CommodityCategory()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.CommodityCategoryDataNotFound, TestConstants.CommodityId));
                x.ErrorId.Should().Be(RuleErrorId.CommodityCategoryDataNotFound);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenSpeciesTypeNameIsNotValidForCommodity()
    {
        // Arrange
        const string speciesTypeName = "Some species type name";

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
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCct)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(speciesTypeName)
                        }
                    }
                }
            }
        };

        var spsCertificate = BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityCategories(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityCategory>.Success(BuildCommodityCategory()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.SpeciesTypeNameNotRecognised, speciesTypeName));
                x.ErrorId.Should().Be(RuleErrorId.SpeciesTypeNameNotRecognised);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenSpeciesClassNameIsNotValidForCommodity()
    {
        // Arrange
        const string speciesClassName = "Some species class name";

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
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCct)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesTypeName)
                        }
                    },
                    new ApplicableSpsClassification
                    {
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcc)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(speciesClassName)
                        }
                    },
                    new ApplicableSpsClassification
                    {
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcf)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesFamilyName)
                        }
                    }
                }
            }
        };

        var spsCertificate = BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityCategories(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityCategory>.Success(BuildCommodityCategory()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.SpeciesClassNameNotRecognised, speciesClassName));
                x.ErrorId.Should().Be(RuleErrorId.SpeciesClassNameNotRecognised);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenSpeciesFamilyNameIsNotValidForCommodity()
    {
        // Arrange
        const string speciesFamilyName = "Some species family name";

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
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCct)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesTypeName)
                        }
                    },
                    new ApplicableSpsClassification
                    {
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcc)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesClassName)
                        }
                    },
                    new ApplicableSpsClassification
                    {
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcf)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(speciesFamilyName)
                        }
                    }
                }
            }
        };

        var spsCertificate = BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityCategories(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityCategory>.Success(BuildCommodityCategory()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.SpeciesFamilyNameNotRecognised, speciesFamilyName));
                x.ErrorId.Should().Be(RuleErrorId.SpeciesFamilyNameNotRecognised);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenSpeciesNameIsNotValidForCommodity()
    {
        // Arrange
        const string speciesName = "Some species name";

        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                SequenceNumeric = SpsCertificateTestHelper.BuildSequenceNumericWithValue(1),
                ScientificName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(speciesName)
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
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCct)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesTypeName)
                        }
                    },
                    new ApplicableSpsClassification
                    {
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcf)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesFamilyName)
                        }
                    },
                    new ApplicableSpsClassification
                    {
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcc)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesClassName)
                        }
                    }
                }
            }
        };

        var spsCertificate = BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityCategories(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityCategory>.Success(BuildCommodityCategory()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.SpeciesNameNotRecognised, speciesName));
                x.ErrorId.Should().Be(RuleErrorId.SpeciesNameNotRecognised);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_DoesNotAddError_WhenSpeciesInformationIsValidForCommodity()
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
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCct)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesTypeName)
                        }
                    },
                    new ApplicableSpsClassification
                    {
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcc)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesClassName)
                        }
                    },
                    new ApplicableSpsClassification
                    {
                        SystemName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcf)
                        },
                        ClassName = new List<TextType>
                        {
                            SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesFamilyName)
                        }
                    }
                }
            }
        };

        var spsCertificate = BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        _commodityCodeServiceMock
            .Setup(x => x.GetCommodityCategories(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Result<CommodityCategory>.Success(BuildCommodityCategory()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }

    private static SpsCertificate BuildSpsCertificateWithTradeLineItems(List<IncludedSpsTradeLineItem> includedSpsTradeLineItems)
    {
        return new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                IncludedSpsConsignmentItem = new List<IncludedSpsConsignmentItem>
                {
                    new IncludedSpsConsignmentItem
                    {
                        IncludedSpsTradeLineItem = includedSpsTradeLineItems
                    }
                }
            },
            SpsExchangedDocument = new SpsExchangedDocument
            {
                IncludedSpsNote = new List<SpsNoteType>
                {
                    SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Chedp)
                }
            }
        };
    }

    private static CommodityCategory BuildCommodityCategory()
    {
        var commodityCategoryInfo = new CommodityCategoryInfo
        {
            Types = new List<Type>
            {
                new Type { Text = TestConstants.SpeciesTypeName, Value = "1" }
            },
            TaxonomicClasses = new List<TaxonomicClass>
            {
                new TaxonomicClass { Type = "1", Value = "1", Text = TestConstants.SpeciesClassName }
            },
            TaxonomicFamilies = new List<TaxonomicFamily>
            {
                new TaxonomicFamily { Clazz = "1", Value = "1", Text = TestConstants.SpeciesFamilyName }
            },
            TaxonomicModels = new List<TaxonomicModel>
            {
                new TaxonomicModel { Family = "1", Value = "1" }
            },
            TaxonomicSpecies = new List<TaxonomicSpecies>
            {
                new TaxonomicSpecies { Text = TestConstants.ScientificName, Model = "1" }
            }
        };

        return new CommodityCategory
        {
            Data = JsonSerializer.Serialize(commodityCategoryInfo)
        };
    }
}
