namespace STW.ProcessingApi.Function.Services;

using System.Net.Http.Json;
using Interfaces;
using Microsoft.Extensions.Logging;
using Models;
using Models.Commodity;

public class CommodityCodeService : ICommodityCodeService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CommodityCodeService> _logger;

    public CommodityCodeService(HttpClient httpClient, ILogger<CommodityCodeService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Result<CommodityInfo>> GetCommodityInfoBySpeciesName(string commodityCode, string speciesName)
    {
        var uri = $"commodity-species/chedpp/{commodityCode}?speciesName={speciesName}&exactMatch=true";
        var result = await _httpClient.GetAsync(uri);

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError($"{nameof(GetCommodityInfoBySpeciesName)} has received an unsuccessful status code: {(int)result.StatusCode}");

            return Result<CommodityInfo>.Failure();
        }

        var commodityInfo = await result.Content.ReadFromJsonAsync<CommodityInfo>();

        return Result<CommodityInfo>.Success(commodityInfo!);
    }

    public async Task<Result<CommodityInfo>> GetCommodityInfoByEppoCode(string commodityCode, string eppoCode)
    {
        var uri = $"commodity-species/chedpp/{commodityCode}?eppoCode={eppoCode}&exactMatch=true";
        var result = await _httpClient.GetAsync(uri);

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError($"{nameof(GetCommodityInfoByEppoCode)} has received an unsuccessful status code: {(int)result.StatusCode}");

            return Result<CommodityInfo>.Failure();
        }

        var commodityInfo = await result.Content.ReadFromJsonAsync<CommodityInfo>();

        return Result<CommodityInfo>.Success(commodityInfo!);
    }

    public async Task<Result<IList<CommodityConfiguration>>> GetCommodityConfigurations(IList<string> commodityCodes)
    {
        var uri = $"commodity-codes/chedpp/commodity-configuration?commodityCodes={string.Join(",", commodityCodes)}";
        var result = await _httpClient.GetAsync(uri);

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError($"{nameof(GetCommodityConfigurations)} has received an unsuccessful status code: {(int)result.StatusCode}");

            return Result<IList<CommodityConfiguration>>.Failure();
        }

        var commodityConfigurations = await result.Content.ReadFromJsonAsync<List<CommodityConfiguration>>();

        return Result<IList<CommodityConfiguration>>.Success(commodityConfigurations!);
    }

    public async Task<Result<CommodityCategory>> GetCommodityCategories(string commodityCode, string chedType)
    {
        var uri = $"commodity-categories/{chedType}-{commodityCode}";
        var result = await _httpClient.GetAsync(uri);

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError($"{nameof(GetCommodityCategories)} has received an unsuccessful status code: {(int)result.StatusCode}");

            return Result<CommodityCategory>.Failure();
        }

        var commodityCategory = await result.Content.ReadFromJsonAsync<CommodityCategory>();

        return Result<CommodityCategory>.Success(commodityCategory!);
    }
}
