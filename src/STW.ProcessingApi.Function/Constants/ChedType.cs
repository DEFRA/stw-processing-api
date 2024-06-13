namespace STW.ProcessingApi.Function.Constants;

public static class ChedType
{
    public const string Cheda = "CHEDA";
    public const string Chedp = "CHEDP";
    public const string Chedd = "CHEDD";
    public const string Chedpp = "CHEDPP";

    public static readonly IReadOnlyList<string> Values =
    [
        Cheda,
        Chedp,
        Chedd,
        Chedpp
    ];

    public static string ToOldType(string chedType)
    {
        return chedType switch
        {
            Cheda => "CVEDA",
            Chedp => "CVEDP",
            Chedd => "CED",
            Chedpp => "CHEDPP",
            _ => throw new ArgumentOutOfRangeException(nameof(chedType), chedType, null)
        };
    }

    public static string ToDashed(string chedType)
    {
        return chedType switch
        {
            Cheda => "CHED-A",
            Chedp => "CHED-P",
            Chedd => "CHED-D",
            Chedpp => "CHED-PP",
            _ => throw new ArgumentOutOfRangeException(nameof(chedType), chedType, null)
        };
    }
}
