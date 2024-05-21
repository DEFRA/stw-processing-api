namespace STW.ProcessingApi.Function.Services.Interfaces;

using Models;
using Models.Bcp;

public interface IBcpService
{
    Task<Result<List<Bcp>>> GetBcpsWithCodeAndType(string code, string chedType);
}
