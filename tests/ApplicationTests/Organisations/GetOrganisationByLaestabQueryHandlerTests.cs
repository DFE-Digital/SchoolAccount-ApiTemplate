using Application.Organisations.GetByLaestab;
using NSubstitute;
using SharedKernel;
using Shouldly;

namespace ApplicationTests.Organisations;

public class GetOrganisationByLaestabQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Populated_OrganisationResponse()
    {
        // arrange
        string laCode = "321";
        string estNo = "4567";
        string laestab = laCode + estNo;
        
        IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
        dateTimeProvider.UtcNow.Returns(new DateTime(2026, 7, 17, 13, 0, 0, DateTimeKind.Utc));
        var handler = new GetOrganisationByLaestabQueryHandler(dateTimeProvider);
        
        // act
        Result<OrganisationResponse> result =
            await handler.Handle(new GetOrganisationByLaestabQuery(laestab), CancellationToken.None);
        
        // assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Status.ShouldBe(OrgStatus.Open);
        result.Value.LocalAuthorityCode.ShouldBe(laCode);
        result.Value.EstablishmentNo.ShouldBe(estNo);
    }
}
