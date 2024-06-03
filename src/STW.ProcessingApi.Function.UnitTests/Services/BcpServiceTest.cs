using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using STW.ProcessingApi.Function.Models;
using STW.ProcessingApi.Function.Services;
using STW.ProcessingApi.Function.UnitTests.TestExtensions;

namespace STW.ProcessingApi.Function.UnitTests.Services;

[TestClass]
public class BcpServiceTest
{
    [TestMethod]
    public async Task GetBcpsWithCodeAndType_ReturnsBcps_WhenValues()
    {
        // Arrange
        var bcp = new Bcp
        {
            Id = 1,
            Code = "CODE",
            Name = "Name",
            Suspended = false,
        };

        var httpMessageHandler = new Mock<HttpMessageHandler>();
        httpMessageHandler
            .SetupSendAsync(HttpMethod.Get, "https://bcp.service/bcps/search?code=CODE&type=CHED_TYPE")
            .ReturnsHttpResponseAsync(
                new BcpSearchResponse
                {
                    Bcps = [bcp],
                },
                HttpStatusCode.OK);

        var httpClient = new HttpClient(httpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://bcp.service"),
        };

        var bcpService = new BcpService(httpClient);

        // Act
        var result = await bcpService.GetBcpsWithCodeAndType("CODE", "CHED_TYPE");

        // Assert
        result.Should().Equal(bcp);
    }

    [TestMethod]
    public async Task GetBcpsWithCodeAndType_ReturnsEmptyList_WhenNoValues()
    {
        // Arrange
        var httpMessageHandler = new Mock<HttpMessageHandler>();
        httpMessageHandler
            .SetupSendAsync(HttpMethod.Get, "https://bcp.service/bcps/search?code=CODE&type=CHED_TYPE")
            .ReturnsHttpResponseAsync(
                new BcpSearchResponse
                {
                    Bcps = [],
                },
                HttpStatusCode.OK);

        var httpClient = new HttpClient(httpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://bcp.service"),
        };

        var bcpService = new BcpService(httpClient);

        // Act
        var result = await bcpService.GetBcpsWithCodeAndType("CODE", "CHED_TYPE");

        // Assert
        result.Should().BeEmpty();
    }
}