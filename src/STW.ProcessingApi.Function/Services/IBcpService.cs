namespace STW.ProcessingApi.Function.Services;

using Models;

public interface IBcpService
{
    Task<Result<List<Bcp>>> GetBcpsWithCodeAndType(string code, string chedType);
}
