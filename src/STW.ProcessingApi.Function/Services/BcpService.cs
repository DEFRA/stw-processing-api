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

    public async Task<List<Bcp>> GetBcpsWithCodeAndType(string code, string chedType)
    {
        var response =
            await _httpClient.GetFromJsonAsync<BcpSearchResponse>($"/bcps/search?code={code}&type={chedType}");
        return response?.Bcps ?? [];
    }
}
