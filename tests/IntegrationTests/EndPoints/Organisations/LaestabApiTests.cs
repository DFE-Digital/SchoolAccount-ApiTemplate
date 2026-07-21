using Application.Abstractions.Messaging;
using Application.Organisations.GetByLaestab;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NSubstitute;
using Shouldly;
using SharedKernel;

namespace IntegrationTests.EndPoints.Organisations;

public class LaestabApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    private readonly IQueryHandler<GetOrganisationByLaestabQuery, OrganisationResponse> _organisationHandler =
        Substitute.For<IQueryHandler<GetOrganisationByLaestabQuery, OrganisationResponse>>();

    public LaestabApiTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            services.AddScoped<IQueryHandler<GetOrganisationByLaestabQuery, OrganisationResponse>>(_ =>
                _organisationHandler))).CreateClient();
    }

    [Fact]
    public async Task GetOrganisationByLaestab_Returns_OrganisationResponse()
    {
        // Arrange
        string laestab = "1234567";
        var expectedResponse = new OrganisationResponse
        {
            LocalAuthorityCode = "Test Organisation",
            EstablishmentNo = "1234567",
            Status = OrgStatus.Open
        };

        _organisationHandler.Handle(Arg.Any<GetOrganisationByLaestabQuery>(), CancellationToken.None)
            .Returns(Result.Success(expectedResponse));

        // Act
        HttpResponseMessage response =
            await client.GetAsync($"/organisations/{laestab}", CancellationToken.None);

        // Assert
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }
}
