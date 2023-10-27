using MediatR;
using TodoListBackend.Database;
using TodoListBackend.Models;

namespace TodoListBackend.Commands;

public record EditTodoCommand(Todo todo) : IRequest<Unit>;

public class EditTodoCommandHandler : IRequestHandler<EditTodoCommand, Unit>
{
    public EditTodoCommandHandler(IDBContext context)
    {
        _context = context;
    }

    public Task<Unit> Handle(EditTodoCommand request, CancellationToken cancellationToken)
    {
        _context.Todos.Update(request.todo);
        _context.SaveChanges();

        return Unit.Task;
    }

    private readonly IDBContext _context;
}
