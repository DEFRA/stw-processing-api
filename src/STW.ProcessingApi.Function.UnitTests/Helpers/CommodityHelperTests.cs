namespace STW.ProcessingApi.Function.UnitTests.Helpers;

using Constants;
using FluentAssertions;
using Function.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class CommodityHelperTests
{
    [TestMethod]
    public void GetCommodityId_ReturnsCommodityId_WhenMatchingSystemNameIsFound()
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
        var result = CommodityHelper.GetCommodityId(applicableSpsClassifications);

        // Assert
        result.Should().Be(TestConstants.CommodityId);
    }

    [TestMethod]
    public void GetCommodityId_ReturnsNull_WhenNoMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>();

        // Act
        var result = CommodityHelper.GetCommodityId(applicableSpsClassifications);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void GetEppoCode_ReturnsEppoCode_WhenMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>
        {
            new ApplicableSpsClassification
            {
                SystemId = SpsCertificateTestHelper.BuildIdTypeWithValue(SystemId.Eppo),
                ClassCode = SpsCertificateTestHelper.BuildCodeTypeWithValue(TestConstants.EppoCode)
            }
        };

        // Act
        var result = CommodityHelper.GetEppoCode(applicableSpsClassifications);

        // Assert
        result.Should().Be(TestConstants.EppoCode);
    }

    [TestMethod]
    public void GetEppoCode_ReturnsNull_WhenNoMatchingSystemNameIsFound()
    {
        // Arrange
        var applicableSpsClassifications = new List<ApplicableSpsClassification>();

        // Act
        var result = CommodityHelper.GetEppoCode(applicableSpsClassifications);

        // Assert
        result.Should().BeNull();
    }
}
