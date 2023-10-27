using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoListBackend.Database;
using TodoListBackend.Models;

namespace TodoListBackend.Queries;


// Task didn't specify we didn't need paginators so no paginators for now
public record GetTodosQuery() : IRequest<List<Todo>>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, List<Todo>>
{

    public GetTodosQueryHandler(IDBContext context)
    {
        _context = context;
    }

    public Task<List<Todo>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return _context.Todos.ToListAsync();
    }
    private readonly IDBContext _context;
}
