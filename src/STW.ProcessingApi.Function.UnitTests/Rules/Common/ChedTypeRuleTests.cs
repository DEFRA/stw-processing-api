namespace STW.ProcessingApi.Function.UnitTests.Rules.Common;

using Constants;
using FluentAssertions;
using Function.Rules.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class ChedTypeRuleTests
{
    private ChedTypeRule _systemUnderTest;
    private List<ErrorEvent> _errorEvents;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new ChedTypeRule();
        _errorEvents = new List<ErrorEvent>();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsTrue_WhenCalled()
    {
        // Arrange
        var spsCertificate = new SpsCertificate();

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow(ChedType.Chedp)]
    [DataRow(ChedType.Chedpp)]
    public void Invoke_DoesNotAddError_WhenChedTypeIsPresentAndValid(string chedType)
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, chedType)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_AddsError_WhenCertificateTypeIsInvalid()
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, TestConstants.Invalid)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.ChedTypeInvalid));
    }

    [TestMethod]
    public void Invoke_AddsError_WhenCertificateTypeIsMissing()
    {
        // Arrange
        var spsCertificate = new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument()
        };

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.ChedTypeMissing));
    }
}
