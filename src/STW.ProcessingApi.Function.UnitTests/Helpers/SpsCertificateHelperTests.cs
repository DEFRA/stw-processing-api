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
    public void GetSpsNoteTypeBySubjectCode_ReturnsCorrectSpsNoteType_WhenCalledWithSubjectCodeConformsToEu()
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
    public void GetChedType_ReturnsCorrectSpsNoteTypeValue_WhenCalledWithSubjectCodeChedType()
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

    [TestMethod]
    public void GetApplicableSpsClassificationBySystemId_ReturnsCorrectApplicableSpsClassification_WhenMatchingSystemIdIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>
        {
            new ApplicableSpsClassification
            {
                SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Cn),
                ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.CommodityId)
            }
        };

        // Act
        var result = SpsCertificateHelper.GetApplicableSpsClassificationBySystemId(applicableSpsClassifications, SystemId.Cn);

        // Assert
        result.Should().Be(applicableSpsClassifications[0]);
    }

    [TestMethod]
    public void GetApplicableSpsClassificationBySystemId_ReturnsNull_WhenNoMatchingSystemIdIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>();

        // Act
        var result = SpsCertificateHelper.GetApplicableSpsClassificationBySystemId(applicableSpsClassifications, SystemId.Cn);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetApplicableSpsClassificationBySystemName_ReturnsCorrectApplicableSpsClassification_WhenMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>
        {
            new ApplicableSpsClassification
            {
                SystemName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcc),
                },
                ClassName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesClassName)
                }
            }
        };

        // Act
        var result = SpsCertificateHelper.GetApplicableSpsClassificationBySystemName(applicableSpsClassifications, SystemName.IpaffsCcc);

        // Assert
        result.Should().Be(applicableSpsClassifications[0]);
    }

    [TestMethod]
    public void GetApplicableSpsClassificationBySystemName_ReturnsNull_WhenNoMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>();

        // Act
        var result = SpsCertificateHelper.GetApplicableSpsClassificationBySystemName(applicableSpsClassifications, SystemName.IpaffsCcc);

        // Assert
        result.Should().BeNull();
    }
}
