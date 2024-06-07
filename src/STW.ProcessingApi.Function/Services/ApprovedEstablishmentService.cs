namespace STW.ProcessingApi.Function.Services;

using System.Net.Http.Json;
using Interfaces;
using Microsoft.Extensions.Logging;
using Models;
using Models.ApprovedEstablishment;

public class ApprovedEstablishmentService : IApprovedEstablishmentService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApprovedEstablishmentService> _logger;

    public ApprovedEstablishmentService(HttpClient httpClient, ILogger<ApprovedEstablishmentService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Result<PageImpl<ApprovedEstablishment>>> Search(ApprovedEstablishmentSearchQuery searchQuery)
    {
        var result = await _httpClient.PostAsJsonAsync("approved-establishment/search?skip=0&numberOfResults=25", searchQuery);

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError($"Approved Establishment Search has received an unsuccessful status code: {(int)result.StatusCode}.");

            return Result<PageImpl<ApprovedEstablishment>>.Failure();
        }

        var approvedEstablishmentPageImpl = await result.Content.ReadFromJsonAsync<PageImpl<ApprovedEstablishment>>();

        return Result<PageImpl<ApprovedEstablishment>>.Success(approvedEstablishmentPageImpl!);
    }
}
