namespace STW.ProcessingApi.Function.Validation.Rules;

using Models;
using Services;

public class BcpValidRule : AsyncRule
{
    private readonly IBcpService _bcpService;

    public BcpValidRule(IBcpService bcpService)
    {
        _bcpService = bcpService;
    }

    public override async Task<List<ValidationError>> Validate(SpsCertificate spsCertificate)
    {
        var chedType = GetChedType(spsCertificate);
        var bcpCode = GetBcpCode(spsCertificate);

        var bcps = await _bcpService.GetBcpsWithCodeAndType(bcpCode, chedType);

        if (bcps.Count == 0)
        {
            Errors.Add(new ValidationError($"Invalid BCP with code {bcpCode} for CHED type {chedType}"));
        }
        else if (bcps[0].Suspended)
        {
            Errors.Add(new ValidationError($"BCP with code {bcpCode} for CHED type {chedType} is suspended"));
        }

        return Errors;
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
