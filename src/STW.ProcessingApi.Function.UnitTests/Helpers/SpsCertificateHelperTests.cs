namespace STW.ProcessingApi.Function.UnitTests.Helpers;

using Constants;
using FluentAssertions;
using Function.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class SpsCertificateHelperTests
{
    [TestMethod]
    public void GetSpsNoteTypeBySubjectCode_ReturnsCorrectSpsNote_WhenCalledWithSubjectCodeConformsToEu()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.True)
        };

        // Act
        var result = SpsCertificateHelper.GetSpsNoteTypeBySubjectCode(spsNoteTypes, SubjectCode.ConformsToEu);

        // Assert
        result.Should().Be(spsNoteTypes[0]);
    }

    [TestMethod]
    public void GetSpsNoteTypeBySubjectCode_ReturnsNull_WhenNoMatchingSubjectCodeIsFound()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = SpsCertificateHelper.GetSpsNoteTypeBySubjectCode(spsNoteTypes, SubjectCode.NonConformingGoodsDestinationRegisteredNumber);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetSpsNoteTypeContentValueBySubjectCode_ReturnsCorrectValue_WhenCalledWithSubjectCodeConformsToEu()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.True)
        };

        // Act
        var result = SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.ConformsToEu);

        // Assert
        result.Should().Be(TestConstants.True);
    }

    [TestMethod]
    public void GetSpsNoteTypeContentValueBySubjectCode_ReturnsNull_WhenNoMatchingSubjectCodeIsFound()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.NonConformingGoodsDestinationRegisteredNumber);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetChedType_ReturnsCorrectValue_WhenCalledWithSubjectCodeChedType()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Chedp)
        };

        // Act
        var result = SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.ChedType);

        // Assert
        result.Should().Be(ChedType.Chedp);
    }

    [TestMethod]
    public void GetChedType_ReturnsNull_WhenNoMatchingSubjectCodeIsFound()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.ChedType);

        // Assert
        result.Should().BeNull();
    }
}
