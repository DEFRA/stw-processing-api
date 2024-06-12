namespace STW.ProcessingApi.Function.UnitTests.Helpers;

using System.Text.Json;
using FluentAssertions;
using Function.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Commodity;

[TestClass]
public class CommodityCategoryParserTests
{
    [TestMethod]
    [DataRow(default)]
    [DataRow("")]
    public void Parse_ReturnsEmptyList_WhenCommodityInfoIsNullOrAndEmptyString(string commodityInfo)
    {
        // Act
        var result = CommodityCategoryParser.Parse(commodityInfo);

        // Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void Parse_ReturnsEmptyList_WhenExceptionIsThrownDuringDeserialization()
    {
        // Arrange
        const string commodityInfo = "{";

        // Act
        var result = CommodityCategoryParser.Parse(commodityInfo);

        // Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void Parse_ReturnsCorrectTypeInfoList_WhenCalledWithValidCommodityInfoString()
    {
        // Arrange
        var commodityCategoryInfo = new CommodityCategoryInfo
        {
            Types = new List<Type>
            {
                new Type { Text = "Species Type Name 1", Value = "1" },
                new Type { Text = "Species Type Name 2", Value = "2" }
            },
            TaxonomicClasses = new List<TaxonomicClass>
            {
                new TaxonomicClass { Type = "1", Value = "1", Text = "Species Class Name 1" },
                new TaxonomicClass { Type = "2", Value = "2", Text = "Species Class Name 2" }
            },
            TaxonomicFamilies = new List<TaxonomicFamily>
            {
                new TaxonomicFamily { Clazz = "1", Value = "1", Text = "Species Family Name 1" },
                new TaxonomicFamily { Clazz = "2", Value = "2", Text = "Species Family Name 2" }
            },
            TaxonomicModels = new List<TaxonomicModel>
            {
                new TaxonomicModel { Family = "1", Value = "1", Text = "Species Model Name 1" },
                new TaxonomicModel { Family = "2", Value = "2", Text = "Species Model Name 2" }
            },
            TaxonomicSpecies = new List<TaxonomicSpecies>
            {
                new TaxonomicSpecies { Text = "Species Name 1", Model = "1" },
                new TaxonomicSpecies { Text = "Species Name 2", Model = "2" }
            }
        };

        // Act
        var result = CommodityCategoryParser.Parse(JsonSerializer.Serialize(commodityCategoryInfo));

        // Assert
        result.Should().HaveCount(2).And.SatisfyRespectively(
            x =>
            {
                x.Type.Should().BeEquivalentTo(commodityCategoryInfo.Types[0]);
                x.TaxonomicClasses.Should().HaveCount(1).And.SatisfyRespectively(z => z.Should().BeEquivalentTo(commodityCategoryInfo.TaxonomicClasses[0]));
                x.TaxonomicSpecies.Should().HaveCount(1).And.SatisfyRespectively(z => z.Should().BeEquivalentTo(commodityCategoryInfo.TaxonomicSpecies[0]));
                x.TaxonomicFamilies.Should().HaveCount(1).And.SatisfyRespectively(z => z.Should().BeEquivalentTo(commodityCategoryInfo.TaxonomicFamilies[0]));
                x.TaxonomicModels.Should().HaveCount(1).And.SatisfyRespectively(z => z.Should().BeEquivalentTo(commodityCategoryInfo.TaxonomicModels[0]));
            },
            x =>
            {
                x.Type.Should().BeEquivalentTo(commodityCategoryInfo.Types[1]);
                x.TaxonomicClasses.Should().HaveCount(1).And.SatisfyRespectively(z => z.Should().BeEquivalentTo(commodityCategoryInfo.TaxonomicClasses[1]));
                x.TaxonomicSpecies.Should().HaveCount(1).And.SatisfyRespectively(z => z.Should().BeEquivalentTo(commodityCategoryInfo.TaxonomicSpecies[1]));
                x.TaxonomicFamilies.Should().HaveCount(1).And.SatisfyRespectively(z => z.Should().BeEquivalentTo(commodityCategoryInfo.TaxonomicFamilies[1]));
                x.TaxonomicModels.Should().HaveCount(1).And.SatisfyRespectively(z => z.Should().BeEquivalentTo(commodityCategoryInfo.TaxonomicModels[1]));
            });
    }
}
