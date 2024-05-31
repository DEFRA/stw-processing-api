using STW.ProcessingApi.Function.Models;

namespace STW.ProcessingApi.Function.Services;

public interface IBcpService
{
    Task<List<Bcp>> GetBcpsWithCodeAndType(string code, string chedType);
}
