namespace STW.ProcessingApi.Function.Extensions;

public static class DecimalExtensions
{
    public static decimal RemoveTrailingZeros(this decimal value)
    {
        return value / 1.0m;
    }

    public static int Precision(this decimal value)
    {
        var decimalStr = value.ToString("G29");
        var parts = decimalStr.Split('.');
        var scale = parts.Length > 1 ? parts[1].Length : 0;
        var precision = parts[0].Replace("-", string.Empty).Length + scale;

        return precision;
    }
}
