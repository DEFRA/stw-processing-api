namespace STW.ProcessingApi.Function.Helpers;

using System.Linq;
using Newtonsoft.Json.Linq;

public static class FieldConfigHelper
{
    public static List<string> GetValidPurposes(string fieldConfigData, string? speciesTypeName)
    {
        var internalMarketKey = string.IsNullOrEmpty(speciesTypeName) ? "internalMarket" : $"internalMarket-{speciesTypeName}";
        var expression = $"$..['Purpose']..[?(@.id=='{internalMarketKey}' || @.name=='{internalMarketKey}')]..values";
        var array = (JArray)JToken.Parse(fieldConfigData).SelectTokens(expression).FirstOrDefault() ?? [];
        var internalMarketPurposes = new List<string>();
        if (array.Count > 0)
        {
            internalMarketPurposes = array
                .Select(element => ((JObject)element).GetValue("label")!.ToString())
                .ToList();
        }

        return internalMarketPurposes;
    }
}
