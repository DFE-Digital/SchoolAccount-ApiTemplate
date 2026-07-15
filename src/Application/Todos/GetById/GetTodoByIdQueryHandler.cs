using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Todos.GetById;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.AllConstructors)]
internal sealed class GetTodoByIdQueryHandler : IQueryHandler<GetTodoByIdQuery, TodoResponse>
{
    public async Task<Result<TodoResponse>> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new TodoResponse());
    }
}
