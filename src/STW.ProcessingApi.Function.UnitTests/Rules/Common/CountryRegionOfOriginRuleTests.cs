namespace STW.ProcessingApi.Function.UnitTests.Rules.Common;

using Constants;
using FluentAssertions;
using Function.Rules.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using TestHelpers;

[TestClass]
public class CountryRegionOfOriginRuleTests
{
    private CountryRegionOfOriginRule _systemUnderTest;
    private List<ErrorEvent> _errorEvents;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new CountryRegionOfOriginRule();
        _errorEvents = new List<ErrorEvent>();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeNotChedppOrChedp()
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, TestConstants.Invalid)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    [DataRow(ChedType.Chedpp)]
    [DataRow(ChedType.Chedp)]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedppOrChedp(string chedType)
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
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Invoke_AddsError_WhenTradeLineItemDoesNotContainOriginSpsCountry()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem()
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(RuleErrorMessage.CountryOfOriginMissing));
    }

    [TestMethod]
    public void Invoke_AddsError_WhenTradeLineItemsContainMultipleCountriesOfOrigin()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.AfghanistanIsoCode)
                }
            },
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.AlbaniaIsoCode)
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.MoreThanOneCountryOfOrigin, "AF, AL")));
    }

    [TestMethod]
    public void Invoke_AddsError_WhenTradeLineItemContainsMultipleRegionsOfOrigin()
    {
        // Arrange
        var spsCountryType = SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndSubDivisionName(TestConstants.UnitedKingdomIsoCode, TestConstants.ScotlandRegionCode);
        spsCountryType.SubordinateSpsCountrySubDivision.Add(SpsCertificateTestHelper.BuildSpsCountrySubDivisionTypeWithName(TestConstants.WalesRegionCode));

        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    spsCountryType
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.MoreThanOneRegionOfOriginInTradeLineItem, "GB-SCO, GB-WLS")));
    }

    [TestMethod]
    public void Invoke_AddsError_WhenConsignmentContainsMultipleRegionsOfOriginAcrossMultipleTradeLineItems()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndSubDivisionName(TestConstants.UnitedKingdomIsoCode, TestConstants.ScotlandRegionCode)
                }
            },
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndSubDivisionName(TestConstants.UnitedKingdomIsoCode, TestConstants.WalesRegionCode)
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.MoreThanOneRegionOfOriginInConsignment, "GB-SCO, GB-WLS")));
    }

    [TestMethod]
    public void Invoke_AddsError_WhenRegionOfOriginDoesNotHavePrefixOfCountryOfOriginCode()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndSubDivisionName(TestConstants.UnitedKingdomIsoCode, TestConstants.Invalid)
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().HaveCount(1).And.SatisfyRespectively(
            x => x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.InvalidRegionOfOrigin, TestConstants.Invalid)));
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenTradeLineItemsHaveSameCountryOfOriginAndHaveNoRegionOfOrigin()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.AfghanistanIsoCode)
                }
            },
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithId(TestConstants.AfghanistanIsoCode)
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }

    [TestMethod]
    public void Invoke_DoesNotAddError_WhenTradeLineItemsHaveSameCountryOfOriginAndRegionOfOrigin()
    {
        // Arrange
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndSubDivisionName(TestConstants.UnitedKingdomIsoCode, TestConstants.ScotlandRegionCode)
                }
            },
            new IncludedSpsTradeLineItem
            {
                OriginSpsCountry = new List<SpsCountryType>
                {
                    SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndSubDivisionName(TestConstants.UnitedKingdomIsoCode, TestConstants.ScotlandRegionCode)
                }
            }
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);

        // Act
        _systemUnderTest.Invoke(spsCertificate, _errorEvents);

        // Assert
        _errorEvents.Should().BeEmpty();
    }
}
