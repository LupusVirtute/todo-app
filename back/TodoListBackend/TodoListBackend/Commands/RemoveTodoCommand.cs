using MediatR;
using TodoListBackend.Database;
using TodoListBackend.Models;

namespace TodoListBackend.Commands;

public record RemoveTodoCommand(int todoId) : IRequest<Unit>;

public class RemoveTodoCommandHandler : IRequestHandler<RemoveTodoCommand, Unit>
{

    public RemoveTodoCommandHandler(IDBContext dBContext)
    {
        _context = dBContext;
    }

    public Task<Unit> Handle(RemoveTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo {
            Id = request.todoId,
        };
        _context.Todos.Attach(todo);
        _context.Todos.Remove(todo);
        _context.SaveChanges();
        return Unit.Task;
    }

    private readonly IDBContext _context;
}
