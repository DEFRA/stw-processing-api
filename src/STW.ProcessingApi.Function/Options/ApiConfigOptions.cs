namespace STW.ProcessingApi.Function.Options;

public class ApiConfigOptions
{
    public const string Section = "ApiConfig";

    public int Timeout { get; set; }

    public string ApprovedEstablishmentBaseUrl { get; set; }

    public string BcpServiceBaseUrl { get; set; }

    public string CommodityCodeBaseUrl { get; set; }
}
