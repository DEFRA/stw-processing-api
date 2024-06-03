namespace STW.ProcessingApi.Function.Services;

using Models;

public interface IBcpService
{
    Task<List<Bcp>> GetBcpsWithCodeAndType(string code, string chedType);
}
