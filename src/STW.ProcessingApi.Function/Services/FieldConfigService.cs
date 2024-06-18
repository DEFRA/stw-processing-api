namespace STW.ProcessingApi.Function.Services;

using System.Net.Http.Json;
using Interfaces;
using Models;

public class FieldConfigService : IFieldConfigService
{
    private readonly HttpClient _httpClient;

    public FieldConfigService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<FieldConfigDto>> GetByCertTypeAndCommodityCode(string certType, string commodityCode)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<FieldConfigDto>($"configurations/v2/{certType}-{commodityCode}");
            return Result<FieldConfigDto>.Success(response!);
        }
        catch (HttpRequestException exception)
        {
            return Result<FieldConfigDto>.Failure(exception);
        }
    }
}
