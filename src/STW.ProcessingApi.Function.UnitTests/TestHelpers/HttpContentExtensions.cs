namespace STW.ProcessingApi.Function.UnitTests.TestHelpers;

using System.Net.Mime;
using System.Text;
using System.Text.Json;

public static class HttpContentExtensions
{
    public static StringContent ToJsonContent(this object obj)
    {
        var serializedString = JsonSerializer.Serialize(obj);

        return new StringContent(serializedString, Encoding.UTF8, MediaTypeNames.Application.Json);
    }
}
