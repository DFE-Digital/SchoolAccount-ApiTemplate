using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Organisation.GetByLaestab;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetOrganisationByLaestabQueryHandler(IDateTimeProvider dateTimeProvider) : IQueryHandler<GetOrganisationByLaestabQuery, OrganisationResponse>
{
    public async Task<Result<OrganisationResponse>> Handle(GetOrganisationByLaestabQuery query, CancellationToken cancellationToken)
    {
        string[] parts = query.laestab.Split('-');
        
        if (parts.Length != 2 || !int.TryParse(parts[0], out int localAuthorityCode) || !int.TryParse(parts[1], out int establishmentNo))
        {
            return Result.Failure<OrganisationResponse>(Error.Failure("InvalidLaestab", "Laestab format is invalid"));
        }

        var response = new OrganisationResponse
        {
            LocalAuthorityCode = localAuthorityCode,
            EstablishmentNo = establishmentNo,
            Status = dateTimeProvider.UtcNow.Hour >= 8 && dateTimeProvider.UtcNow.Hour < 15 ? OrgStatus.Open : OrgStatus.Closed
        };

        return await Task.FromResult(Result.Success(response));
    }
}
