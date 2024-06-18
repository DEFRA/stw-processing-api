namespace STW.ProcessingApi.Function.Helpers;

using System.Linq;
using Newtonsoft.Json.Linq;

public static class FieldConfigHelper
{
    public static List<string> GetValidPurposes(string fieldConfigData, string? speciesTypeName)
    {
        var internalMarketKey = string.IsNullOrEmpty(speciesTypeName)
            ? "internalMarket"
            : $"internalMarket-{speciesTypeName}";
        var expression = $"$..['Purpose']..[?(@.id=='{internalMarketKey}' || @.name=='{internalMarketKey}')]..values";

        var internalMarketPurposes = JToken.Parse(fieldConfigData)
            .SelectTokens(expression)
            .FirstOrDefault()?
            .Select(x => ((JObject)x).GetValue("label")!.ToString())
            .ToList();

        return internalMarketPurposes ?? [];
    }
}
