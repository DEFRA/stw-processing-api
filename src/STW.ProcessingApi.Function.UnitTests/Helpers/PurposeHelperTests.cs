namespace STW.ProcessingApi.Function.UnitTests.Helpers;

using Constants;
using FluentAssertions;
using Function.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class PurposeHelperTests
{
    [TestMethod]
    public void ConsignmentConformsToEu_ReturnsFalse_WhenSpsNoteTypeIsNotPresent()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = PurposeHelper.ConsignmentConformsToEu(spsNoteTypes);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void ConsignmentConformsToEu_ReturnsFalse_WhenSpsNoteTypeContentIsNotValidBooleanValue()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.Invalid)
        };

        // Act
        var result = PurposeHelper.ConsignmentConformsToEu(spsNoteTypes);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void ConsignmentConformsToEu_ReturnsTrue_WhenSpsNoteTypeContentIsTrue()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.True)
        };

        // Act
        var result = PurposeHelper.ConsignmentConformsToEu(spsNoteTypes);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void ConsignmentConformsToEu_ReturnsFalse_WhenSpsNoteTypeContentIsFalse()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ConformsToEu, TestConstants.False)
        };

        // Act
        var result = PurposeHelper.ConsignmentConformsToEu(spsNoteTypes);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void GetNonConformingGoodsDestinationRegisteredNumber_ReturnsNull_WhenSpsNoteTypeIsNotPresent()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = PurposeHelper.GetNonConformingGoodsDestinationRegisteredNumber(spsNoteTypes);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetNonConformingGoodsDestinationRegisteredNumber_ReturnsRegisteredNumber_WhenSpsNoteTypeIsPresent()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(
                SubjectCode.NonConformingGoodsDestinationRegisteredNumber,
                TestConstants.DestinationRegisteredNumber)
        };

        // Act
        var result = PurposeHelper.GetNonConformingGoodsDestinationRegisteredNumber(spsNoteTypes);

        // Assert
        result.Should().Be(TestConstants.DestinationRegisteredNumber);
    }

    [TestMethod]
    public void GetNonConformingGoodsDestinationType_ReturnsNull_WhenSpsNoteTypeIsNotPresent()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = PurposeHelper.GetNonConformingGoodsDestinationType(spsNoteTypes);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetNonConformingGoodsDestinationType_ReturnsDestinationType_WhenSpsNoteTypeIsPresent()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(
                SubjectCode.NonConformingGoodsDestinationType,
                DestinationType.CustomsWarehouse)
        };

        // Act
        var result = PurposeHelper.GetNonConformingGoodsDestinationType(spsNoteTypes);

        // Assert
        result.Should().Be(DestinationType.CustomsWarehouse);
    }

    [TestMethod]
    public void GetPurpose_ReturnsNull_WhenSpsNoteTypeIsNotPresent()
    {
        // Arrange
        var spsAuthenticationTypes = new List<SpsAuthenticationType>();

        // Act
        var result = PurposeHelper.GetPurpose(spsAuthenticationTypes);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetPurpose_ReturnsPurpose_WhenSpsNoteTypeIsPresent()
    {
        // Arrange
        var spsAuthenticationTypes = new List<SpsAuthenticationType>
        {
            new SpsAuthenticationType
            {
                IncludedSpsClause = new List<IncludedSpsClause>
                {
                    SpsCertificateTestHelper.BuildIncludedSpsClauseWithIdAndContent(SubjectCode.Purpose, Purpose.NonConformingGoods)
                }
            }
        };

        // Act
        var result = PurposeHelper.GetPurpose(spsAuthenticationTypes);

        // Assert
        result.Should().Be(Purpose.NonConformingGoods);
    }

    [TestMethod]
    public void GetNonConformingGoodsDestinationShipName_ReturnsNull_WhenSpsNoteTypeIsNotPresent()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = PurposeHelper.GetNonConformingGoodsDestinationShipName(spsNoteTypes);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetNonConformingGoodsDestinationShipName_ReturnsShipName_WhenSpsNoteTypeIsPresent()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationShipName, TestConstants.ShipName)
        };

        // Act
        var result = PurposeHelper.GetNonConformingGoodsDestinationShipName(spsNoteTypes);

        // Assert
        result.Should().Be(TestConstants.ShipName);
    }

    [TestMethod]
    public void GetNonConformingGoodsDestinationShipPort_ReturnsNull_WhenSpsNoteTypeIsNotPresent()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>();

        // Act
        var result = PurposeHelper.GetNonConformingGoodsDestinationShipPort(spsNoteTypes);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetNonConformingGoodsDestinationShipPort_ReturnsShipPort_WhenSpsNoteTypeIsPresent()
    {
        // Arrange
        var spsNoteTypes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.NonConformingGoodsDestinationShipPort, TestConstants.ShipPort)
        };

        // Act
        var result = PurposeHelper.GetNonConformingGoodsDestinationShipPort(spsNoteTypes);

        // Assert
        result.Should().Be(TestConstants.ShipPort);
    }
}
