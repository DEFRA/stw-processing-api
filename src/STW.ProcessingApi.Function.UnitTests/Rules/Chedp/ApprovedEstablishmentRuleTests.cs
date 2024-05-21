namespace STW.ProcessingApi.Function.UnitTests.Rules.Chedp;

using System.Net;
using AutoFixture;
using AutoFixture.AutoMoq;
using Constants;
using FluentAssertions;
using Function.Rules.Chedp;
using Function.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.ApprovedEstablishment;
using Moq;
using TestHelpers;

[TestClass]
public class ApprovedEstablishmentRuleTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private IList<ValidationError> _validationErrors;
    private Mock<IApprovedEstablishmentService> _approvedEstablishmentServiceMock;
    private ApprovedEstablishmentRule _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _validationErrors = new List<ValidationError>();
        _approvedEstablishmentServiceMock = new Mock<IApprovedEstablishmentService>();
        _systemUnderTest = new ApprovedEstablishmentRule(_approvedEstablishmentServiceMock.Object);
    }

    [TestMethod]
    [DataRow(ChedType.Chedpp)]
    [DataRow(ChedType.Cheda)]
    [DataRow(ChedType.Chedd)]
    [DataRow(TestConstants.Invalid)]
    public void ShouldInvoke_ReturnsFalse_WhenChedTypeNotChedp(string chedType)
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, chedType)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void ShouldInvoke_ReturnsTrue_WhenChedTypeIsChedp()
    {
        // Arrange
        var includedSpsNotes = new List<SpsNoteType>
        {
            SpsCertificateTestHelper.BuildSpsNoteTypeWithSubjectCodeAndContent(SubjectCode.ChedType, ChedType.Chedp)
        };

        var spsCertificate = SpsCertificateTestHelper.BuildSpsCertificateWithIncludedSpsNotes(includedSpsNotes);

        // Act
        var result = _systemUnderTest.ShouldInvoke(spsCertificate);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public async Task InvokeAsync_AddsNoErrors_WhenApprovedEstablishmentServiceRespondsWithSuccessAndHasApprovedEstablishments()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _approvedEstablishmentServiceMock
            .Setup(x => x.Search(It.IsAny<ApprovedEstablishmentSearchQuery>()))
            .ReturnsAsync(Result<PageImpl<ApprovedEstablishment>>.Success(_fixture.Create<PageImpl<ApprovedEstablishment>>()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().BeEmpty();

        _approvedEstablishmentServiceMock.Verify(
            x =>
                x.Search(
                    It.Is<ApprovedEstablishmentSearchQuery>(
                        m =>
                            m.ApprovalNumber == TestConstants.OperatorId
                            && m.ActivitiesLegend == TestConstants.Slaughterhouses
                            && m.CountryCode == TestConstants.NewZealandIsoCode
                            && m.Section == TestConstants.OperatorRoleCode)),
            Times.Once);
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenApprovedEstablishmentServiceRespondsWithSuccessAndHasNoApprovedEstablishments()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _approvedEstablishmentServiceMock
            .Setup(x => x.Search(It.IsAny<ApprovedEstablishmentSearchQuery>()))
            .ReturnsAsync(Result<PageImpl<ApprovedEstablishment>>.Success(new PageImpl<ApprovedEstablishment>()));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(string.Format(RuleErrorMessage.ApprovedEstablishmentNotFound, TestConstants.OperatorId));
                x.ErrorId.Should().Be(RuleErrorId.ApprovedEstablishmentNotFound);
            });
    }

    [TestMethod]
    public async Task InvokeAsync_AddsError_WhenApprovedEstablishmentServiceRespondsWithFailure()
    {
        // Arrange
        var spsCertificate = BuildSpsCertificate();

        _approvedEstablishmentServiceMock
            .Setup(x => x.Search(It.IsAny<ApprovedEstablishmentSearchQuery>()))
            .ReturnsAsync(Result<PageImpl<ApprovedEstablishment>>.Failure(HttpStatusCode.InternalServerError));

        // Act
        await _systemUnderTest.InvokeAsync(spsCertificate, _validationErrors);

        // Assert
        _validationErrors.Should().HaveCount(1).And.SatisfyRespectively(
            x =>
            {
                x.ErrorMessage.Should().Be(RuleErrorMessage.ApprovedEstablishmentClientError);
                x.ErrorId.Should().Be(RuleErrorId.ApprovedEstablishmentClientError);
            });
    }

    private static SpsCertificate BuildSpsCertificate()
    {
        var tradeLineItems = new List<IncludedSpsTradeLineItem>
        {
            new IncludedSpsTradeLineItem
            {
                AppliedSpsProcess = new List<AppliedSpsProcess>
                {
                    new AppliedSpsProcess
                    {
                        OperationSpsCountry = SpsCertificateTestHelper.BuildSpsCountryTypeWithIdAndName(TestConstants.NewZealandIsoCode, TestConstants.NewZealand),
                        OperatorSpsParty = new SpsPartyType
                        {
                            Id = SpsCertificateTestHelper.BuildIdTypeWithValue(TestConstants.OperatorId),
                            Name = SpsCertificateTestHelper.BuildTextTypeWithValue(TestConstants.OperatorName),
                            RoleCode = SpsCertificateTestHelper.BuildRoleCodeWithNameAndValue(TestConstants.OperatorRoleCode, RoleCodeValue.AA)
                        },
                        TypeCode = SpsCertificateTestHelper.BuildProcessTypeCodeTypeWithNameAndValue(TestConstants.Slaughterhouses, TestConstants.SlaughterhousesCode)
                    }
                }
            }
        };

        return SpsCertificateTestHelper.BuildSpsCertificateWithTradeLineItems(tradeLineItems);
    }
}
