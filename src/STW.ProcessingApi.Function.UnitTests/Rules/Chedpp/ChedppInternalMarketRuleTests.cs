namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedpp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedpp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class ChedppInternalMarketRuleTests
{
    private ChedppInternalMarketRule _systemUnderTest;
    private List<ErrorEvent> _errorEvents;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new ChedppInternalMarketRule();
        _errorEvents = new List<ErrorEvent>();
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
    [DataRow(Purpose.NonConformingGoods)]
    [DataRow(Purpose.Transhipment)]
    [DataRow(Purpose.DirectTransit)]
    [DataRow(Purpose.ReEntry)]
    public void Invoke_AddError_WhenPurposeIsNotInternalMarket(string purpose)
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause =
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, purpose)
                        }
                    }
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.PurposeMustBeInternalMarket));
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenPurposeIsInternalMarket()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                SignatorySpsAuthentication = new List<SpsAuthenticationType>
                {
                    new SpsAuthenticationType
                    {
                        IncludedSpsClause =
                        {
                            SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.InternalMarket)
                        }
                    }
                }
            }
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }
}
