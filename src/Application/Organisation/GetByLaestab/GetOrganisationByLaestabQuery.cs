using Application.Abstractions.Messaging;

namespace Application.Organisation.GetByLaestab;

public sealed record GetOrganisationByLaestabQuery(string laestab) : IQuery<OrganisationResponse>;
