namespace STW.ProcessingApi.Function.Options;

public class ApiConfigOptions
{
    public const string Section = "ApiConfig";

    public string ApprovedEstablishmentBaseUrl { get; set; }

    public int Timeout { get; set; }
}
