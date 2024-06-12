namespace STW.ProcessingApi.Function.Models.Commodity;

using System.Text.Json.Serialization;

public class TaxonomicModel
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("family")]
    public string Family { get; set; }
}
