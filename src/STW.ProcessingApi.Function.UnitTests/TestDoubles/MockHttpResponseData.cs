namespace STW.ProcessingApi.UnitTests.TestDoubles;

using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

public class MockHttpResponseData : HttpResponseData
{
    public MockHttpResponseData(FunctionContext functionContext, HttpStatusCode status)
        : base(functionContext)
    {
        StatusCode = status;
    }

    public override HttpStatusCode StatusCode { get; set; }

    public override HttpHeadersCollection Headers { get; set; } = new HttpHeadersCollection();

    public override Stream Body { get; set; } = new MemoryStream();

    public override HttpCookies Cookies { get; }
}
