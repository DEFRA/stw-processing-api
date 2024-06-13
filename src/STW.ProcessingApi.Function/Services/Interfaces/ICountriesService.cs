namespace STW.ProcessingApi.Function.Services.Interfaces;

using Models;
using Models.Country;

public interface ICountriesService
{
    Task<Result<Country>> GetCountryOrRegionByIsoCode(string isoCode);
}
