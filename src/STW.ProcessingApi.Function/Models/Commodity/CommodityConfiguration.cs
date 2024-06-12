namespace STW.ProcessingApi.Function.Models.Commodity;

public class CommodityConfiguration
{
    public string CommodityCode { get; set; }

    public bool RequireTestAndTrail { get; set; }

    public bool RequiresFinishedOrPropagated { get; set; }
}
