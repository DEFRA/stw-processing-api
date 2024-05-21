namespace STW.ProcessingApi.Function.UnitTests.Services;

using System.Net;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Function.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Country;
using Moq;
using TestHelpers;

[TestClass]
public class CountriesServiceTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private Mock<ILogger<CountriesService>> _loggerMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private CountriesService _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger<CountriesService>>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(TestConstants.HttpClientBaseAddress)
        };

        _systemUnderTest = new CountriesService(httpClient, _loggerMock.Object);
    }

    [TestMethod]
    public async Task GetCountryOrRegionByIsoCode_ReturnsSuccessResult_WhenHttpClientRespondsWithSuccessfulStatusCode()
    {
        // Arrange
        var country = _fixture.Create<Country>();

        _httpMessageHandlerMock.RespondWith(HttpStatusCode.OK, country.ToJsonContent());

        // Act
        var result = await _systemUnderTest.GetCountryOrRegionByIsoCode(TestConstants.AfghanistanIsoCode);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(country);

        _httpMessageHandlerMock.VerifyRequest(HttpMethod.Get, new Uri($"https://example.com/countries/country-or-region?isoCode={TestConstants.AfghanistanIsoCode}"), Times.Once());
    }

    [TestMethod]
    public async Task GetCountryOrRegionByIsoCode_ReturnsFailureResult_WhenHttpClientRespondsWithUnsuccessfulStatusCode()
    {
        // Arrange
        _httpMessageHandlerMock.RespondWith(HttpStatusCode.InternalServerError, default);

        // Act
        var result = await _systemUnderTest.GetCountryOrRegionByIsoCode(TestConstants.Invalid);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();

        _loggerMock.VerifyLog(x => x.LogError("GetCountryOrRegionByIsoCode has received an unsuccessful status code: 500"));
    }
}
