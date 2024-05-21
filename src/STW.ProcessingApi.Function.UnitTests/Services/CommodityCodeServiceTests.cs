namespace STW.ProcessingApi.Function.UnitTests.Services;

using System.Net;
using AutoFixture;
using AutoFixture.AutoMoq;
using Constants;
using FluentAssertions;
using Function.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Commodity;
using Moq;
using TestHelpers;

[TestClass]
public class CommodityCodeServiceTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private Mock<ILogger<CommodityCodeService>> _loggerMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private CommodityCodeService _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger<CommodityCodeService>>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(TestConstants.HttpClientBaseAddress)
        };

        _systemUnderTest = new CommodityCodeService(httpClient, _loggerMock.Object);
    }

    [TestMethod]
    public async Task GetCommodityInfoBySpeciesName_ReturnsSuccessResult_WhenHttpClientRespondsWithSuccessfulStatusCode()
    {
        // Arrange
        var commodityInfo = _fixture.Create<CommodityInfo>();

        _httpMessageHandlerMock.RespondWith(HttpStatusCode.OK, commodityInfo.ToJsonContent());

        // Act
        var result = await _systemUnderTest.GetCommodityInfoBySpeciesName(TestConstants.CommodityId, TestConstants.ScientificName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(commodityInfo);

        _httpMessageHandlerMock.VerifyRequest(
            HttpMethod.Get,
            new Uri($"https://example.com/commodity-species/chedpp/{TestConstants.CommodityId}?speciesName={TestConstants.ScientificName}&exactMatch=true"),
            Times.Once());
    }

    [TestMethod]
    public async Task GetCommodityInfoBySpeciesName_ReturnsFailureResult_WhenHttpClientRespondsWithUnsuccessfulStatusCode()
    {
        // Arrange
        _httpMessageHandlerMock.RespondWith(HttpStatusCode.InternalServerError, default);

        // Act
        var result = await _systemUnderTest.GetCommodityInfoBySpeciesName(TestConstants.CommodityId, TestConstants.ScientificName);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();

        _loggerMock.VerifyLog(x => x.LogError("GetCommodityInfoBySpeciesName has received an unsuccessful status code: 500"));
    }

    [TestMethod]
    public async Task GetCommodityInfoByEppoCode_ReturnsSuccessResult_WhenHttpClientRespondsWithSuccessfulStatusCode()
    {
        // Arrange
        var commodityInfo = _fixture.Create<CommodityInfo>();

        _httpMessageHandlerMock.RespondWith(HttpStatusCode.OK, commodityInfo.ToJsonContent());

        // Act
        var result = await _systemUnderTest.GetCommodityInfoByEppoCode(TestConstants.CommodityId, TestConstants.EppoCode);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(commodityInfo);

        _httpMessageHandlerMock.VerifyRequest(
            HttpMethod.Get,
            new Uri($"https://example.com/commodity-species/chedpp/{TestConstants.CommodityId}?eppoCode={TestConstants.EppoCode}&exactMatch=true"),
            Times.Once());
    }

    [TestMethod]
    public async Task GetCommodityInfoByEppoCode_ReturnsFailureResult_WhenHttpClientRespondsWithUnsuccessfulStatusCode()
    {
        // Arrange
        _httpMessageHandlerMock.RespondWith(HttpStatusCode.InternalServerError, default);

        // Act
        var result = await _systemUnderTest.GetCommodityInfoByEppoCode(TestConstants.CommodityId, TestConstants.EppoCode);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();

        _loggerMock.VerifyLog(x => x.LogError("GetCommodityInfoByEppoCode has received an unsuccessful status code: 500"));
    }

    [TestMethod]
    public async Task GetCommodityConfigurations_ReturnsSuccessResult_WhenHttpClientRespondsWithSuccessfulStatusCode()
    {
        // Arrange
        var commodityInfo = _fixture.Create<List<CommodityConfiguration>>();

        _httpMessageHandlerMock.RespondWith(HttpStatusCode.OK, commodityInfo.ToJsonContent());

        // Act
        var result = await _systemUnderTest.GetCommodityConfigurations([TestConstants.CommodityId]);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(commodityInfo);

        _httpMessageHandlerMock.VerifyRequest(
            HttpMethod.Get,
            new Uri($"https://example.com/commodity-codes/chedpp/commodity-configuration?commodityCodes={TestConstants.CommodityId}"),
            Times.Once());
    }

    [TestMethod]
    public async Task GetCommodityConfigurations_ReturnsFailureResult_WhenHttpClientRespondsWithUnsuccessfulStatusCode()
    {
        // Arrange
        _httpMessageHandlerMock.RespondWith(HttpStatusCode.InternalServerError, default);

        // Act
        var result = await _systemUnderTest.GetCommodityConfigurations([TestConstants.CommodityId]);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();

        _loggerMock.VerifyLog(x => x.LogError("GetCommodityConfigurations has received an unsuccessful status code: 500"));
    }

    [TestMethod]
    public async Task GetCommodityCategories_ReturnsSuccessResult_WhenHttpClientRespondsWithSuccessfulStatusCode()
    {
        // Arrange
        var commodityInfo = _fixture.Create<CommodityCategory>();

        _httpMessageHandlerMock.RespondWith(HttpStatusCode.OK, commodityInfo.ToJsonContent());

        // Act
        var result = await _systemUnderTest.GetCommodityCategories(TestConstants.CommodityId, ChedType.Chedpp);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(commodityInfo);

        _httpMessageHandlerMock.VerifyRequest(
            HttpMethod.Get,
            new Uri($"https://example.com/commodity-categories/{ChedType.Chedpp}-{TestConstants.CommodityId}"),
            Times.Once());
    }

    [TestMethod]
    public async Task GetCommodityCategories_ReturnsFailureResult_WhenHttpClientRespondsWithUnsuccessfulStatusCode()
    {
        // Arrange
        _httpMessageHandlerMock.RespondWith(HttpStatusCode.InternalServerError, default);

        // Act
        var result = await _systemUnderTest.GetCommodityCategories(TestConstants.CommodityId, ChedType.Chedpp);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();

        _loggerMock.VerifyLog(x => x.LogError("GetCommodityCategories has received an unsuccessful status code: 500"));
    }
}
