namespace STW.ProcessingApi.Function.Services.Interfaces;

using Models;

public interface IFieldConfigService
{
    Task<Result<FieldConfigDto>> GetByCertTypeAndCommodityCode(string certType, string commodityCode);
}
