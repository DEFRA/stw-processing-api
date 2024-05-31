using System.Net.Http.Json;
using STW.ProcessingApi.Function.Models;

namespace STW.ProcessingApi.Function.Services;

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
