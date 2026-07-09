using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Users.GetById;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetUserByIdQueryHandler()
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new UserResponse());
    }
}
