using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Users.GetByEmail;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetUserByEmailQueryHandler
    : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new UserResponse());
    }
}
