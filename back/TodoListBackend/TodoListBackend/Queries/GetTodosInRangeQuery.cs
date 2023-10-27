using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoListBackend.Database;
using TodoListBackend.Models;

namespace TodoListBackend.Queries;

public record GetTodosInRangeQuery(DateTime from, DateTime to) : IRequest<List<Todo>>;

public class GetTodosInRangeQueryHandler : IRequestHandler<GetTodosInRangeQuery, List<Todo>>
{

    public GetTodosInRangeQueryHandler(IDBContext context)
    {
        _context = context;
    }


    public Task<List<Todo>> Handle(GetTodosInRangeQuery request, CancellationToken cancellationToken)
    {
        var (dateFrom, dateTo) = request;

        return _context.Todos.Where(x => x.TodoDate > dateFrom && x.TodoDate < dateTo).ToListAsync();
    }

    private readonly IDBContext _context;
}
