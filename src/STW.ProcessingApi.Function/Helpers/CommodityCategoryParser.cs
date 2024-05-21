namespace STW.ProcessingApi.Function.Helpers;

using System.Text.Json;
using Models.Commodity;

public static class CommodityCategoryParser
{
    public static List<TypeInfo> Parse(string? categoryInfo)
    {
        if (string.IsNullOrEmpty(categoryInfo))
        {
            return new List<TypeInfo>();
        }

        try
        {
            var commodityCategoryInfo = JsonSerializer.Deserialize<CommodityCategoryInfo>(categoryInfo);

            return commodityCategoryInfo?.Types.Select(x => CreateTypeInfo(x, commodityCategoryInfo)).ToList() ?? new List<TypeInfo>();
        }
        catch (JsonException ex)
        {
            return new List<TypeInfo>();
        }
    }

    private static TypeInfo CreateTypeInfo(Type type, CommodityCategoryInfo commodityCategoryInfo)
    {
        var classes = GetClasses(commodityCategoryInfo, type);
        var families = GetFamilies(commodityCategoryInfo, classes.Select(x => x.Value).ToList());
        var models = GetModels(commodityCategoryInfo, families.Select(x => x.Value).ToList());
        var species = GetSpecies(commodityCategoryInfo, models.Select(x => x.Value).ToList());

        return new TypeInfo
        {
            Type = type,
            TaxonomicClasses = classes,
            TaxonomicFamilies = families,
            TaxonomicModels = models,
            TaxonomicSpecies = species
        };
    }

    private static List<TaxonomicClass> GetClasses(CommodityCategoryInfo commodityCategoryInfo, Type type)
    {
        return commodityCategoryInfo.TaxonomicClasses
            .Where(x => x.Type == type.Value)
            .ToList();
    }

    private static List<TaxonomicModel> GetModels(CommodityCategoryInfo commodityCategoryInfo, List<string> familyValues)
    {
        return commodityCategoryInfo.TaxonomicModels
            .Where(x => familyValues.Contains(x.Family))
            .ToList();
    }

    private static List<TaxonomicFamily> GetFamilies(CommodityCategoryInfo commodityCategoryInfo, List<string> classValues)
    {
        return commodityCategoryInfo.TaxonomicFamilies
            .Where(x => classValues.Contains(x.Clazz))
            .ToList();
    }

    private static List<TaxonomicSpecies> GetSpecies(CommodityCategoryInfo commodityCategoryInfo, List<string> modelValues)
    {
        return commodityCategoryInfo.TaxonomicSpecies
            .Where(x => modelValues.Contains(x.Model))
            .ToList();
    }
}
