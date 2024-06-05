namespace STW.ProcessingApi.Function.Options;

public class ApiConfigOptions
{
    public const string Section = "ApiConfig";

    public string BcpServiceBaseUrl { get; set; }

    public int Timeout { get; set; }
}
