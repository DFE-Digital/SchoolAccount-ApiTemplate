using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using Application.Organisation.GetByLaestab;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Organisation;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetByLaestab : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("organisation/{laestab:regex(^\\d{{7}}$)}", async (
                int laestab,
                IQueryHandler<GetOrganisationByLaestabQuery, OrganisationResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetOrganisationByLaestabQuery(laestab);

                Result<OrganisationResponse> result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Organisation);
    }
}
