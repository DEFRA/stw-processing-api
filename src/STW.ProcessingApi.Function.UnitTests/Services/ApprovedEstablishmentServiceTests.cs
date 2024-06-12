namespace STW.ProcessingApi.Function.UnitTests.Services;

using System.Net;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Function.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.ApprovedEstablishment;
using Moq;
using TestHelpers;

[TestClass]
public class ApprovedEstablishmentServiceTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private Mock<ILogger<ApprovedEstablishmentService>> _loggerMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private ApprovedEstablishmentService _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger<ApprovedEstablishmentService>>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(TestConstants.HttpClientBaseAddress)
        };

        _systemUnderTest = new ApprovedEstablishmentService(httpClient, _loggerMock.Object);
    }

    [TestMethod]
    public async Task Search_ReturnsSuccessResult_WhenHttpClientRespondsWithSuccessfulStatusCode()
    {
        // Arrange
        var approvedEstablishments = _fixture.Create<PageImpl<ApprovedEstablishment>>();

        _httpMessageHandlerMock.RespondWith(HttpStatusCode.OK, approvedEstablishments.ToJsonContent());

        // Act
        var result = await _systemUnderTest.Search(new ApprovedEstablishmentSearchQuery());

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(approvedEstablishments);

        _httpMessageHandlerMock.VerifyRequest(HttpMethod.Post, new Uri("https://example.com/approved-establishment/search?skip=0&numberOfResults=25"), Times.Once());
    }

    [TestMethod]
    public async Task Search_ReturnsFailureResult_WhenHttpClientRespondsWithUnsuccessfulStatusCode()
    {
        // Arrange
        _httpMessageHandlerMock.RespondWith(HttpStatusCode.InternalServerError, default);

        // Act
        var result = await _systemUnderTest.Search(new ApprovedEstablishmentSearchQuery());

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();

        _loggerMock.VerifyLog(x => x.LogError("Search has received an unsuccessful status code: 500"));
    }
}
