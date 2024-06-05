namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class ApprovedEstablishmentsOfOriginRuleTests
{
    private ApprovedEstablishmentsOfOriginRule _systemUnderTest;
    private List<ValidationError> _validationErrors;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new ApprovedEstablishmentsOfOriginRule();
        _validationErrors = new List<ValidationError>();
    }

    [TestMethod]
    [DataRow(ChedType.Chedpp)]
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
    public void Invoke_DoesNotAddError_WhenAllExpectedFieldsAreProvided()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                AppliedSpsProcess = new List<AppliedSpsProcess>
                {
                    new AppliedSpsProcess
                    {
                        OperationSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndName(TestConstants.NewZealandIsoCode, TestConstants.NewZealand),
                        OperatorSpsParty = new SpsPartyType
                        {
                            Id = SpsCertificateTestHelper.BuildIdTypeWithValue(TestConstants.OperatorId),
                            Name = SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.OperatorName),
                            RoleCode = SpsCertificateTestHelper.BuildRoleCodeWithNameAndValue(TestConstants.OperatorRoleCode, RoleCodeValue.ZZZ)
                        },
                        TypeCode = SpsCertificateTestHelper.BuildProcessTypeCodeTypeWithNameAndValue(TestConstants.Slaughterhouses, TestConstants.SlaughterhousesCode)
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

    [TestMethod]
    public void Invoke_AddsErrors_WhenAllExpectedFieldsHaveEmptyValues()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                AppliedSpsProcess = new List<AppliedSpsProcess>
                {
                    new AppliedSpsProcess
                    {
                        OperationSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndName(string.Empty, string.Empty),
                        OperatorSpsParty = new SpsPartyType
                        {
                            Id = SpsCertificateTestHelper.BuildIdTypeWithValue(string.Empty),
                            Name = SpsCertificateTestHelper.BuildTextTypeWithValue(string.Empty),
                            RoleCode = SpsCertificateTestHelper.BuildRoleCodeWithNameAndValue(string.Empty, RoleCodeValue.AA)
                        },
                        TypeCode = SpsCertificateTestHelper.BuildProcessTypeCodeTypeWithNameAndValue(string.Empty, string.Empty)
                    }
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(7).And.SatisfyRespectively(
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.ApprovedEstablishmentMissingCountryCode);
                x.Id.Should().Be(RuleErrorId.ApprovedEstablishmentMissingCountryCode);
            },
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.ApprovedEstablishmentMissingCountryName);
                x.Id.Should().Be(RuleErrorId.ApprovedEstablishmentMissingCountryName);
            },
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.ApprovedEstablishmentMissingApprovalNumber);
                x.Id.Should().Be(RuleErrorId.ApprovedEstablishmentMissingApprovalNumber);
            },
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.ApprovedEstablishmentMissingOperatorName);
                x.Id.Should().Be(RuleErrorId.ApprovedEstablishmentMissingOperatorName);
            },
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.ApprovedEstablishmentMissingSection);
                x.Id.Should().Be(RuleErrorId.ApprovedEstablishmentMissingSection);
            },
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.ApprovedEstablishmentMissingActivityName);
                x.Id.Should().Be(RuleErrorId.ApprovedEstablishmentMissingActivityName);
            },
            x =>
            {
                x.Message.Should().Be(RuleErrorMessage.ApprovedEstablishmentMissingActivityCode);
                x.Id.Should().Be(RuleErrorId.ApprovedEstablishmentMissingActivityCode);
            });
    }

    [TestMethod]
    public void Invoke_AddsError_WhenExpectedRoleCodeValueIsIncorrect()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                AppliedSpsProcess = new List<AppliedSpsProcess>
                {
                    new AppliedSpsProcess
                    {
                        OperationSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndName(TestConstants.NewZealandIsoCode, TestConstants.NewZealand),
                        OperatorSpsParty = new SpsPartyType
                        {
                            Id = SpsCertificateTestHelper.BuildIdTypeWithValue(TestConstants.OperatorId),
                            Name = SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.OperatorName),
                            RoleCode = SpsCertificateTestHelper.BuildRoleCodeWithNameAndValue(TestConstants.OperatorRoleCode, RoleCodeValue.AA)
                        },
                        TypeCode = SpsCertificateTestHelper.BuildProcessTypeCodeTypeWithNameAndValue(TestConstants.Slaughterhouses, TestConstants.SlaughterhousesCode)
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
                x.Message.Should().Be(RuleErrorMessage.ApprovedEstablishmentIncorrectRoleCode);
                x.Id.Should().Be(RuleErrorId.ApprovedEstablishmentIncorrectRoleCode);
            });
    }
}
