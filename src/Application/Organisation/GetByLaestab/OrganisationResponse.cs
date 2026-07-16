namespace Application.Organisation.GetByLaestab;

public enum OrgStatus
{
    Open = 0, Closed = 1
}

public sealed record OrganisationResponse
{
    public int LocalAuthorityCode { get; init; }
    public int EstablishmentNo { get; init; }
    public OrgStatus Status { get; init; }
}
