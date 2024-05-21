namespace STW.ProcessingApi.Function.Constants;

public class GoodsCertifiedAs
{
    public const string AnimalFeedingStuff = "ANIMAL_FEEDING_STUFF";
    public const string HumanConsumption = "HUMAN_CONSUMPTION";
    public const string Other = "OTHER";
    public const string PharmaceuticalUse = "PHARMACEUTICAL_USE";
    public const string TechnicalUse = "TECHNICAL_USE";

    public static List<string> Values() =>
    [
        AnimalFeedingStuff,
        HumanConsumption,
        Other,
        PharmaceuticalUse,
        TechnicalUse
    ];
}
