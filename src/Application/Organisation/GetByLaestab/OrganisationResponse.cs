namespace Application.Organisation.GetByLaestab;

public enum OrgStatus
{
    Open = 0, Closed = 1
}

public sealed record OrganisationResponse
{
    public string LocalAuthorityCode { get; init; }
    public string EstablishmentNo { get; init; }
    public OrgStatus Status { get; init; }
}
