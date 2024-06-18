namespace STW.ProcessingApi.Function.UnitTests.Services;

using System.Net;
using Constants;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using STW.ProcessingApi.Function.Services;
using TestHelpers;

[TestClass]
public class FieldConfigServiceTest
{
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private FieldConfigService _fieldConfigService;

    [TestInitialize]
    public void TestInitialize()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://fieldconfig.service")
        };
        _fieldConfigService = new FieldConfigService(httpClient);
    }

    [TestMethod]
    public async Task GetByCertTypeAndCommodityCode_ReturnsFieldConfigDto_WhenValues()
    {
        // Arrange
        var fieldConfigDto = new FieldConfigDto
        {
            CertificateType = ChedType.Chedp,
            CommodityCode = "123",
            Data = "data",
            Id = 1
        };

        _httpMessageHandlerMock.RespondWith(HttpStatusCode.OK, fieldConfigDto.ToJsonContent());

        // Act
        var result = await _fieldConfigService.GetByCertTypeAndCommodityCode("certType", "commodityCode");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(fieldConfigDto);
    }

    [TestMethod]
    public async Task GetByCertTypeAndCommodityCode_ReturnsError_WhenError()
    {
        // Arrange
        _httpMessageHandlerMock.RespondWith(HttpStatusCode.BadRequest, default);

        // Act
        var result = await _fieldConfigService.GetByCertTypeAndCommodityCode("certType", "commodityCode");

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeOfType<HttpRequestException>()
            .Which.Message.Should().Be("Response status code does not indicate success: 400 (Bad Request).");
    }
}
