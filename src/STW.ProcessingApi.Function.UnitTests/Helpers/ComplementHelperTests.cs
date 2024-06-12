namespace STW.ProcessingApi.Function.UnitTests.Helpers;

using Constants;
using FluentAssertions;
using Function.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class ComplementHelperTests
{
    [TestMethod]
    public void GetVariety_ReturnsVariety_WhenMatchingSubjectCodeIsFound()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.Variety, TestConstants.Variety)
        };

        // Act
        var result = ComplementHelper.GetVariety(spsNoteTypes);

        // Assert
        result.Should().Be(TestConstants.Variety);
    }

    [TestMethod]
    public void GetVariety_ReturnsNull_WhenNoMatchingSubjectCodeIsFound()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = ComplementHelper.GetVariety(spsNoteTypes);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetClass_ReturnsClass_WhenMatchingSubjectCodeIsFound()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.Class, TestConstants.Class)
        };

        // Act
        var result = ComplementHelper.GetClass(spsNoteTypes);

        // Assert
        result.Should().Be(TestConstants.Class);
    }

    [TestMethod]
    public void GetClass_ReturnsNull_WhenNoMatchingSubjectCodeIsFound()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = ComplementHelper.GetClass(spsNoteTypes);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetFinishedOrPropagated_ReturnsFinishedOrPropagatedValue_WhenMatchingSubjectCodeIsFound()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.FinishedOrPropagated, FinishedOrPropagated.Finished)
        };

        // Act
        var result = ComplementHelper.GetFinishedOrPropagated(spsNoteTypes);

        // Assert
        result.Should().Be(FinishedOrPropagated.Finished);
    }

    [TestMethod]
    public void GetFinishedOrPropagated_ReturnsNull_WhenNoMatchingSubjectCodeIsFound()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = ComplementHelper.GetFinishedOrPropagated(spsNoteTypes);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetSpeciesTypeName_ReturnsSpeciesTypeName_WhenMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>
        {
            new ApplicableSpsClassification
            {
                SystemName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCct),
                },
                ClassName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesTypeName)
                }
            }
        };

        // Act
        var result = ComplementHelper.GetSpeciesTypeName(applicableSpsClassifications);

        // Assert
        result.Should().Be(TestConstants.SpeciesTypeName);
    }

    [TestMethod]
    public void GetSpeciesTypeName_ReturnsNull_WhenNoMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>();

        // Act
        var result = ComplementHelper.GetSpeciesTypeName(applicableSpsClassifications);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetSpeciesClassName_ReturnsSpeciesClassName_WhenMatchingSystemNameIsFound()
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
        var result = ComplementHelper.GetSpeciesClassName(applicableSpsClassifications);

        // Assert
        result.Should().Be(TestConstants.SpeciesClassName);
    }

    [TestMethod]
    public void GetSpeciesClassName_ReturnsNull_WhenNoMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>();

        // Act
        var result = ComplementHelper.GetSpeciesClassName(applicableSpsClassifications);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetSpeciesFamilyName_ReturnsSpeciesFamilyName_WhenMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>
        {
            new ApplicableSpsClassification
            {
                SystemName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(SystemName.IpaffsCcf),
                },
                ClassName = new List<TextType>
                {
                    SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.SpeciesFamilyName)
                }
            }
        };

        // Act
        var result = ComplementHelper.GetSpeciesFamilyName(applicableSpsClassifications);

        // Assert
        result.Should().Be(TestConstants.SpeciesFamilyName);
    }

    [TestMethod]
    public void GetSpeciesFamilyName_ReturnsNull_WhenNoMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>();

        // Act
        var result = ComplementHelper.GetSpeciesFamilyName(applicableSpsClassifications);

        // Assert
        result.Should().BeNull();
    }
}
