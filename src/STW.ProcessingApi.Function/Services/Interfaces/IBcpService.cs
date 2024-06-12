namespace STW.ProcessingApi.Function.Services.Interfaces;

using STW.ProcessingApi.Function.Models;

public interface IBcpService
{
    Task<Result<List<Bcp>>> GetBcpsWithCodeAndType(string code, string chedType);
}
