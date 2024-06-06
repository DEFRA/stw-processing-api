namespace STW.ProcessingApi.Function.UnitTests.TestHelpers;

using System.Net;
using Moq;
using Moq.Protected;

public static class HttpHandlerMockExtensions
{
    public static void RespondWith(this Mock<HttpMessageHandler> mock, HttpStatusCode statusCode, HttpContent? httpContent)
    {
        mock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(
                new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = httpContent
                });
    }

    public static void VerifyRequest(this Mock<HttpMessageHandler> mock, HttpMethod expectedMethod, Uri expectedUri, Times expectedInvocations)
    {
        mock.Protected()
            .Verify(
                "SendAsync",
                expectedInvocations,
                ItExpr.Is<HttpRequestMessage>(r => r.Method == expectedMethod && r.RequestUri == expectedUri),
                ItExpr.IsAny<CancellationToken>());
    }
}
