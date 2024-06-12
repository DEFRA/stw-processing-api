namespace STW.ProcessingApi.Function.Models.Commodity;

using System.Text.Json.Serialization;

public class CommodityCategoryInfo
{
    [JsonPropertyName("types")]
    public List<Type> Types { get; set; }

    [JsonPropertyName("classes")]
    public IList<TaxonomicClass> TaxonomicClasses { get; set; }

    [JsonPropertyName("families")]
    public IList<TaxonomicFamily> TaxonomicFamilies { get; set; }

    [JsonPropertyName("models")]
    public IList<TaxonomicModel> TaxonomicModels { get; set; }

    [JsonPropertyName("species")]
    public IList<TaxonomicSpecies> TaxonomicSpecies { get; set; }
}
