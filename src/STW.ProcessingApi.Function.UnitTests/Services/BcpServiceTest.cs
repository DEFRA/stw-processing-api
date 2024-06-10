namespace STW.ProcessingApi.Function.UnitTests.Services;

using System.Net;
using FluentAssertions;
using Function.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using TestHelpers;

[TestClass]
public class BcpServiceTest
{
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private BcpService _bcpService;

    [TestInitialize]
    public void TestInitialize()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://bcp.service"),
        };
        _bcpService = new BcpService(httpClient);
    }

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

        _httpMessageHandlerMock.RespondWith(HttpStatusCode.OK, new BcpSearchResponse { Bcps = [bcp] });

        // Act
        var result = await _bcpService.GetBcpsWithCodeAndType("CODE", "CHED_TYPE");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Equal(bcp);
    }

    [TestMethod]
    public async Task GetBcpsWithCodeAndType_ReturnsEmptyList_WhenNoValues()
    {
        // Arrange
        _httpMessageHandlerMock.RespondWith(HttpStatusCode.OK, new BcpSearchResponse { Bcps = [] });

        // Act
        var result = await _bcpService.GetBcpsWithCodeAndType("CODE", "CHED_TYPE");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [TestMethod]
    public async Task GetBcpsWithCodeAndType_ReturnsError_WhenError()
    {
        // Arrange
        _httpMessageHandlerMock.RespondWith(HttpStatusCode.BadRequest, default);

        // Act
        var result = await _bcpService.GetBcpsWithCodeAndType("CODE", "CHED_TYPE");

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeOfType<HttpRequestException>()
            .Which.Message.Should().Be("Response status code does not indicate success: 400 (Bad Request).");
    }
}
