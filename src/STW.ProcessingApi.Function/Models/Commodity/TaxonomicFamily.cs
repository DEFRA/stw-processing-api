namespace STW.ProcessingApi.Function.Models.Commodity;

using System.Text.Json.Serialization;

public class TaxonomicFamily
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("clazz")]
    public string Clazz { get; set; }
}
