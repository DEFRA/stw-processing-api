namespace STW.ProcessingApi.Function.Services;

using System.Net.Http.Json;
using Models;

public class BcpService : IBcpService
{
    private readonly HttpClient _httpClient;

    public BcpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<List<Bcp>>> GetBcpsWithCodeAndType(string code, string chedType)
    {
        try
        {
            var response =
                await _httpClient.GetFromJsonAsync<BcpSearchResponse>($"/bcps/search?code={code}&type={chedType}");
            return Result<List<Bcp>>.Success(response?.Bcps ?? []);
        }
        catch (HttpRequestException exception)
        {
            return Result<List<Bcp>>.Failure(exception);
        }
    }
}
