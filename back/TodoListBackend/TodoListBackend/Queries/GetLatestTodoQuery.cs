using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoListBackend.Database;
using TodoListBackend.Models;
using TodoListBackend.Services;

namespace TodoListBackend.Queries;

public record GetLatestTodoQuery() : IRequest<List<Todo>>;

public class GetLatestTodoQueryHandler : IRequestHandler<GetLatestTodoQuery, List<Todo>>
{
    private const string SoonNotifierAmountOfHours = "SoonNotifierAmountOfHours";

    public GetLatestTodoQueryHandler(IConfiguration configuration, IDBContext context, ITimeService timeService)
    {
        _context = context;
        _configuration = configuration;
        _timeService = timeService;
    }

    public Task<List<Todo>> Handle(GetLatestTodoQuery request, CancellationToken cancellationToken)
    {
        var amountOfHoursToNotifyString = _configuration.GetSection("Todos")[SoonNotifierAmountOfHours];

        if (amountOfHoursToNotifyString == null)
        {
            throw new ApplicationException($"Didn't set {nameof(SoonNotifierAmountOfHours)} option in Todos settings");
        }
        var amountOfHoursToNotify = int.Parse(amountOfHoursToNotifyString);

        var now = _timeService.Now;

        var afterAmountOfHoursToNotify = _timeService.Now.AddHours(amountOfHoursToNotify);

        return _context.Todos.Where(x => x.TodoDate > now && x.TodoDate < afterAmountOfHoursToNotify).ToListAsync();
    }

    private readonly IDBContext _context;
    private readonly IConfiguration _configuration;
    private readonly ITimeService _timeService;
}
