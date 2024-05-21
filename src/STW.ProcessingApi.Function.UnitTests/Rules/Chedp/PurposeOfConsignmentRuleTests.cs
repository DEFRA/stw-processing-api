namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class PurposeOfConsignmentRuleTests
{
    private PurposeOfConsignmentRule _systemUnderTest;
    private List<ErrorEvent> _errorEvents;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new PurposeOfConsignmentRule();
        _errorEvents = new List<ErrorEvent>();
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
    public void Invoke_DoesNotAddError_WhenConsignmentConformsToEu()
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.True)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenConsignmentDoesNotConformToEuAndDestinationTypeIsNotNull()
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.False),
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationType, DestinationType.Ship)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_Adds_WhenConsignmentDoesNotConformToEuAndDestinationTypeIsNull()
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.False)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.NonConformingConsignmentMissingDestinationType));
    }
}
