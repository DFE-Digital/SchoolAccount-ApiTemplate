using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Todos.Get;

internal sealed class GetTodosQueryHandler()
    : IQueryHandler<GetTodosQuery, List<TodoResponse>>
{
    public async Task<Result<List<TodoResponse>>> Handle(GetTodosQuery query, CancellationToken cancellationToken)
    {
        return new List<TodoResponse>();
    }
}
