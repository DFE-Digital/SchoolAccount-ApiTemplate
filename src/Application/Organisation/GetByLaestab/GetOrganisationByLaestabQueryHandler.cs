using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using System.Text.RegularExpressions;
using System.Globalization;
using SharedKernel;

namespace Application.Organisation.GetByLaestab;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetOrganisationByLaestabQueryHandler(IDateTimeProvider dateTimeProvider) : IQueryHandler<GetOrganisationByLaestabQuery, OrganisationResponse>
{
    private static readonly Regex LaestabRegex = new(@"^(?<lac>\d+)-(?<est>\d+)$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public async Task<Result<OrganisationResponse>> Handle(GetOrganisationByLaestabQuery query, CancellationToken cancellationToken)
    {
        Match match = LaestabRegex.Match(query.laestab);

        if (!match.Success)
        {
            return Result.Failure<OrganisationResponse>(Error.Failure("InvalidLaestab", "Laestab format is invalid"));
        }

        int localAuthorityCode = int.Parse(match.Groups["lac"].Value, NumberStyles.None, CultureInfo.InvariantCulture);
        int establishmentNo = int.Parse(match.Groups["est"].Value, NumberStyles.None, CultureInfo.InvariantCulture);

        var response = new OrganisationResponse
        {
            LocalAuthorityCode = localAuthorityCode,
            EstablishmentNo = establishmentNo,
            Status = dateTimeProvider.UtcNow.Hour >= 8 && dateTimeProvider.UtcNow.Hour < 15 ? OrgStatus.Open : OrgStatus.Closed
        };

        return await Task.FromResult(Result.Success(response));
    }
}
