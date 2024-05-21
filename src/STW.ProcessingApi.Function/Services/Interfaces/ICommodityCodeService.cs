namespace STW.ProcessingApi.Function.Services.Interfaces;

using Models;
using Models.Commodity;

public interface ICommodityCodeService
{
    Task<Result<CommodityInfo>> GetCommodityInfoBySpeciesName(string commodityCode, string speciesName);

    Task<Result<CommodityInfo>> GetCommodityInfoByEppoCode(string commodityCode, string eppoCode);

    Task<Result<IList<CommodityConfiguration>>> GetCommodityConfigurations(IList<string> commodityCodes);

    Task<Result<CommodityCategory>> GetCommodityCategories(string commodityCode, string chedType);
}
