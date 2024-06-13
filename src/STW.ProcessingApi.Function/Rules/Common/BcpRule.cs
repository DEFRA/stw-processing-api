namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
using Interfaces;
using Models;
using Services.Interfaces;

public class BcpRule : IAsyncRule
{
    private readonly IBcpService _bcpService;

    public BcpRule(IBcpService bcpService)
    {
        _bcpService = bcpService;
    }

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        return ChedType.Values.Contains(
            SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote)!);
    }

    public async Task InvokeAsync(SpsCertificate spsCertificate, IList<ValidationError> errors)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote)!;
        var bcpCode = GetBcpCode(spsCertificate);

        var result = await _bcpService.GetBcpsWithCodeAndType(bcpCode, ChedType.ToDashed(chedType));

        if (!result.IsSuccess)
        {
            errors.Add(
                new ValidationError(
                    string.Format(RuleErrorMessage.BcpServiceError, result.Error.Message),
                    RuleErrorId.BcpServiceError));
            return;
        }

        if (result.Value?.Count == 0)
        {
            errors.Add(
                new ValidationError(
                    string.Format(RuleErrorMessage.InvalidBcpCode, bcpCode, chedType),
                    RuleErrorId.InvalidBcpCode));
            return;
        }

        if (result.Value![0].Suspended)
        {
            errors.Add(
                new ValidationError(
                    string.Format(RuleErrorMessage.BcpSuspended, bcpCode, chedType),
                    RuleErrorId.BcpSuspended));
        }
    }

    private static string GetBcpCode(SpsCertificate spsCertificate)
    {
        return spsCertificate.SpsConsignment.UnloadingBaseportSpsLocation?.Id?.Value ?? string.Empty;
    }
}
