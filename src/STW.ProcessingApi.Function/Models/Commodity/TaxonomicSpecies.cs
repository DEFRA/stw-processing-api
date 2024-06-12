namespace STW.ProcessingApi.Function.Models.Commodity;

using System.Text.Json.Serialization;

public class TaxonomicSpecies
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }
}
