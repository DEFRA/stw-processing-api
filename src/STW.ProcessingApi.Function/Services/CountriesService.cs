namespace STW.ProcessingApi.Function.Services;

using System.Net;
using System.Net.Http.Json;
using Interfaces;
using Microsoft.Extensions.Logging;
using Models;
using Models.Commodity;
using Models.Country;

public class CountriesService : ICountriesService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CountriesService> _logger;

    public CountriesService(HttpClient httpClient, ILogger<CountriesService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Result<Country>> GetCountryOrRegionByIsoCode(string isoCode)
    {
        var uri = $"countries/country-or-region?isoCode={isoCode}";
        var result = await _httpClient.GetAsync(uri);

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError($"{nameof(GetCountryOrRegionByIsoCode)} has received an unsuccessful status code: {(int)result.StatusCode}");

            return Result<Country>.Failure(result.StatusCode);
        }

        var country = await result.Content.ReadFromJsonAsync<Country>();

        return Result<Country>.Success(country!);
    }
}
