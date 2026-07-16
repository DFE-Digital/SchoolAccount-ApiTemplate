using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Organisation.GetByLaestab;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetOrganisationByLaestabQueryHandler(IDateTimeProvider dateTimeProvider)
    : IQueryHandler<GetOrganisationByLaestabQuery, OrganisationResponse>
{
    public async Task<Result<OrganisationResponse>> Handle(GetOrganisationByLaestabQuery query,
        CancellationToken cancellationToken)
    {

        // first 3 digits are local authority code, last 4 are establishment number
        int localAuthorityCode = query.laestab / 10_000;
        int establishmentNo = query.laestab % 10_000;

        var response = new OrganisationResponse
        {
            LocalAuthorityCode = localAuthorityCode,
            EstablishmentNo = establishmentNo,
            Status = GetOpenStatus()
        };

        return await Task.FromResult(Result.Success(response));
    }

    private OrgStatus GetOpenStatus()
    {
        return dateTimeProvider.UtcNow.TimeOfDay >= new System.TimeSpan(8, 0, 0)
               && dateTimeProvider.UtcNow.TimeOfDay <= new System.TimeSpan(15, 30, 0)
            ? OrgStatus.Open
            : OrgStatus.Closed;
    }
}
