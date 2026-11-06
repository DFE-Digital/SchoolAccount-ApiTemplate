namespace SchoolAccount.Web.Api.IntegrationTests.Endpoints.Organisations;

public record ClientOrganisationResponse(
    string LocalAuthorityCode,
    string EstablishmentNo,
    string Status
);
