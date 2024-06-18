namespace STW.ProcessingApi.Function.Options;

public class ApiConfigOptions
{
    public const string Section = "ApiConfig";

    public string ApprovedEstablishmentBaseUrl { get; set; }

    public string BcpServiceBaseUrl { get; set; }

    public string CommodityCodeBaseUrl { get; set; }

    public string CountriesBaseUrl { get; set; }

    public string FieldConfigServiceBaseUrl { get; set; }

    public int Timeout { get; set; }
}
