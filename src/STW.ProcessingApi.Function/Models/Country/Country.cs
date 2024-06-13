namespace STW.ProcessingApi.Function.Models.Country;

public class Country
{
    public string Name { get; set; }

    public string Code { get; set; }

    public int Risk { get; set; }

    public bool Eu { get; set; }

    public bool IpaffsChargeGroup { get; set; }

    public bool PodGroup { get; set; }

    public bool IsLowRiskArticle72 { get; set; }
}
