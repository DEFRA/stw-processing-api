namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedpp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedpp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class ScientificNameAndEppoCodeRuleTests
{
    private ScientificNameAndEppoCodeRule _systemUnderTest;
    private List<ValidationError> _validationErrors;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new ScientificNameAndEppoCodeRule();
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
    public void Invoke_DoesNotAddError_WhenScientificNameIsProvided()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                IncludedSpsConsignmentItem = new List<IncludedSpsConsignmentItem>
                {
                    new IncludedSpsConsignmentItem
                    {
                        IncludedSpsTradeLineItem = new List<IncludedSpsTradeLineItem>
                        {
                            new IncludedSpsTradeLineItem
                            {
                                SequenceNumeric = new SequenceNumeric
                                {
                                    Value = 1
                                },
                                ScientificName = new List<TextType>
                                {
                                    new TextType
                                    {
                                        Value = TestConstants.ScientificName
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenEppoCodeIsProvided()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                IncludedSpsConsignmentItem = new List<IncludedSpsConsignmentItem>
                {
                    new IncludedSpsConsignmentItem
                    {
                        IncludedSpsTradeLineItem = new List<IncludedSpsTradeLineItem>
                        {
                            new IncludedSpsTradeLineItem
                            {
                                SequenceNumeric = new SequenceNumeric
                                {
                                    Value = 1
                                },
                                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                                {
                                    new ApplicableSpsClassification
                                    {
                                        SystemId = new IdType
                                        {
                                            Value = CommodityComplementKey.Eppo
                                        },
                                        ClassCode = new CodeType
                                        {
                                            Value = TestConstants.ClassCode
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenEppoCodeAndScientificNameIsProvided()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                IncludedSpsConsignmentItem = new List<IncludedSpsConsignmentItem>
                {
                    new IncludedSpsConsignmentItem
                    {
                        IncludedSpsTradeLineItem = new List<IncludedSpsTradeLineItem>
                        {
                            new IncludedSpsTradeLineItem
                            {
                                SequenceNumeric = new SequenceNumeric
                                {
                                    Value = 1
                                },
                                ScientificName = new List<TextType>
                                {
                                    new TextType
                                    {
                                        Value = TestConstants.ScientificName
                                    }
                                },
                                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                                {
                                    new ApplicableSpsClassification
                                    {
                                        SystemId = new IdType
                                        {
                                            Value = CommodityComplementKey.Eppo
                                        },
                                        ClassCode = new CodeType
                                        {
                                            Value = TestConstants.ClassCode
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_AddError_WhenNoScientificNameOrEppoCodeAreProvided()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                IncludedSpsConsignmentItem = new List<IncludedSpsConsignmentItem>
                {
                    new IncludedSpsConsignmentItem
                    {
                        IncludedSpsTradeLineItem = new List<IncludedSpsTradeLineItem>
                        {
                            new IncludedSpsTradeLineItem
                            {
                                SequenceNumeric = new SequenceNumeric
                                {
                                    Value = 1
                                }
                            }
                        }
                    }
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.ScientificNameOrEppoCodeMissing, 1));
                x.ErrorId.Should().Be(RuleErrorId.ScientificNameOrEppoCodeMissing);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }

    [TestMethod]
    [DataRow(default)]
    [DataRow("")]
    public void Invoke_AddsError_WhenEppoCodeClassCodeIs(string classCodeValue)
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                IncludedSpsConsignmentItem = new List<IncludedSpsConsignmentItem>
                {
                    new IncludedSpsConsignmentItem
                    {
                        IncludedSpsTradeLineItem = new List<IncludedSpsTradeLineItem>
                        {
                            new IncludedSpsTradeLineItem
                            {
                                SequenceNumeric = new SequenceNumeric
                                {
                                    Value = 1
                                },
                                ApplicableSpsClassification = new List<ApplicableSpsClassification>
                                {
                                    new ApplicableSpsClassification
                                    {
                                        SystemId = new IdType
                                        {
                                            Value = CommodityComplementKey.Eppo
                                        },
                                        ClassCode = new CodeType
                                        {
                                            Value = classCodeValue
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.ScientificNameOrEppoCodeMissing, 1));
                x.ErrorId.Should().Be(RuleErrorId.ScientificNameOrEppoCodeMissing);
                x.ErrorTradeLineItem.Should().Be(1);
            });
    }
}
