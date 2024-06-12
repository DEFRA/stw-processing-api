namespace STW.ProcessingApi.Function.Models.Commodity;

using System.Text.Json.Serialization;

public class TaxonomicClass
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
