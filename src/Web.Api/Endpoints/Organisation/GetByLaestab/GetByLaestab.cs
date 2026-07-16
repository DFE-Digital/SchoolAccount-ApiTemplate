using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using Application.Organisation.GetByLaestab;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Organisation.GetByLaestab;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetByLaestab : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("organisation/{laestab}", async (
                [AsParameters] GetByLaestabRequest request,
                IQueryHandler<GetOrganisationByLaestabQuery, OrganisationResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetOrganisationByLaestabQuery(request.Laestab);

                Result<OrganisationResponse> result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Organisation);
    }
}
