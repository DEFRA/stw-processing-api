namespace STW.ProcessingApi.Function.Rules;

using Interfaces;
using Models;
using Services;

public class BcpValidRule : IAsyncRule
{
    private readonly IBcpService _bcpService;

    public BcpValidRule(IBcpService bcpService)
    {
        _bcpService = bcpService;
    }

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        return true;
    }

    public async Task Invoke(SpsCertificate spsCertificate, IList<ValidationError> errors)
    {
        var chedType = GetChedType(spsCertificate);
        var bcpCode = GetBcpCode(spsCertificate);

        try
        {
            var bcps = await _bcpService.GetBcpsWithCodeAndType(bcpCode, chedType);

            if (bcps.Count == 0)
            {
                errors.Add(new ValidationError($"Invalid BCP with code {bcpCode} for CHED type {chedType}"));
            }
            else if (bcps[0].Suspended)
            {
                errors.Add(new ValidationError($"BCP with code {bcpCode} for CHED type {chedType} is suspended"));
            }
        }
        catch (HttpRequestException exception)
        {
            errors.Add(new ValidationError($"Unable to check BCP validity: {exception.Message}"));
        }
    }

    private static string GetChedType(SpsCertificate spsCertificate)
    {
        return spsCertificate.SpsExchangedDocument.IncludedSpsNote.ToList()
            .Find(note => note.SubjectCode.Value == "CHED_TYPE")!.Content.First().Value!;
    }

    private static string GetBcpCode(SpsCertificate spsCertificate)
    {
        return spsCertificate.SpsConsignment.UnloadingBaseportSpsLocation.Id.Value;
    }
}
