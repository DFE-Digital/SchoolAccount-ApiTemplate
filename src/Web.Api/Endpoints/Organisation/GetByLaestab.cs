using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using Application.Organisation.GetByLaestab;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Organisation;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetByLaestab : IEndpoint
{
    public sealed record Request([FromRoute] int Laestab);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("organisation/{laestab}", async (
                [AsParameters] Request request,
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
