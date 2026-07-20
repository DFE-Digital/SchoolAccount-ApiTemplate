using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Organisations.GetByLaestab;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetOrganisationByLaestabQueryHandler(IDateTimeProvider dateTimeProvider)
    : IQueryHandler<GetOrganisationByLaestabQuery, OrganisationResponse>
{
    public async Task<Result<OrganisationResponse>> Handle(GetOrganisationByLaestabQuery query,
        CancellationToken cancellationToken)
    {
        var laestabValue = new LaestabValue(query.laestab);
        var statusCalculator = new StatusCalculator(dateTimeProvider);
        
        var response = new OrganisationResponse
        {
            LocalAuthorityCode = laestabValue.LocalAuthorityCode,
            EstablishmentNo = laestabValue.EstablishmentNumber,
            Status = statusCalculator.GetOpenStatus(),
        };

        return await Task.FromResult(Result.Success(response));
    }
}
