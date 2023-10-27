using MediatR;
using TodoListBackend.Database;
using TodoListBackend.Models;

namespace TodoListBackend.Commands;
public record AddTodoCommand(string title, string description, DateTime todoDate) : IRequest<Unit>;

public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, Unit>
{

    public AddTodoCommandHandler(IDBContext context)
    {
        _context = context;
    }

    public Task<Unit> Handle(AddTodoCommand request, CancellationToken cancellationToken)
    {
        var (title, description, todoDate) = request;

        _context.Todos.Add(new Todo
        {
            Title = title,
            Description = description,
            TodoDate = todoDate
        });
        _context.SaveChanges();
        return Unit.Task;
    }

    private readonly IDBContext _context;
}
