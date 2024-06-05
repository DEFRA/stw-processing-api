namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
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

    public async Task InvokeAsync(SpsCertificate spsCertificate, IList<ValidationError> errors)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote) ??
                       string.Empty;
        var bcpCode = GetBcpCode(spsCertificate);

        try
        {
            var bcps = await _bcpService.GetBcpsWithCodeAndType(bcpCode, chedType);

            if (bcps.Count == 0)
            {
                errors.Add(
                    new ValidationError(
                        string.Format(RuleErrorMessage.InvalidBcpCode, bcpCode, chedType),
                        RuleErrorId.InvalidBcpCode));
            }
            else if (bcps[0].Suspended)
            {
                errors.Add(
                    new ValidationError(
                        string.Format(RuleErrorMessage.BcpSuspended, bcpCode, chedType),
                        RuleErrorId.BcpSuspended));
            }
        }
        catch (HttpRequestException exception)
        {
            errors.Add(
                new ValidationError(
                    string.Format(RuleErrorMessage.BcpServiceError, exception.Message),
                    RuleErrorId.BcpServiceError));
        }
    }

    private static string GetBcpCode(SpsCertificate spsCertificate)
    {
        return spsCertificate.SpsConsignment.UnloadingBaseportSpsLocation?.Id?.Value ?? string.Empty;
    }
}
