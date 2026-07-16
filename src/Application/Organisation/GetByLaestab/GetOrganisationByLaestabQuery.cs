using Application.Abstractions.Messaging;

namespace Application.Organisation.GetByLaestab;

public sealed record GetOrganisationByLaestabQuery(int laestab) : IQuery<OrganisationResponse>;
