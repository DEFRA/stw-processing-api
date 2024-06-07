namespace STW.ProcessingApi.Function.Services.Interfaces;

using Models;
using Models.ApprovedEstablishment;

public interface IApprovedEstablishmentService
{
    Task<Result<PageImpl<ApprovedEstablishment>>> Search(ApprovedEstablishmentSearchQuery searchQuery);
}
