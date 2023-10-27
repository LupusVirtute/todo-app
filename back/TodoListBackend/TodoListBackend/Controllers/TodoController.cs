using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoListBackend.Commands;
using TodoListBackend.Models;
using TodoListBackend.Queries;

namespace TodoListBackend.Controllers;


[ApiController]
[Route("/api")]
public class TodoController : Controller
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;

    }
   
    [HttpGet("[controller]/latest")]
    public async Task<IActionResult> GetLatest()
    {
        return Ok(await _mediator.Send(new GetLatestTodoQuery()));
    }

    [HttpGet("[controller]/all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetTodosQuery()));
    }

    [HttpGet("[controller]")]
    public async Task<IActionResult> GetInRange([FromQuery] GetTodosInRangeQuery getTodosInRangeQuery)
    {
        return Ok(await _mediator.Send(getTodosInRangeQuery));
    }

    [HttpDelete("[controller]/{todoId}")]
    public async Task<IActionResult> Delete([FromRoute] RemoveTodoCommand removeTodoCommand)
    {
        return Ok(await _mediator.Send(removeTodoCommand));
    }

    [HttpPost("[controller]")]
    public async Task<IActionResult> Add([FromForm] AddTodoCommand addTodoCommand)
    {
        return Ok(await _mediator.Send(addTodoCommand));
    }

    [HttpPost("[controller]/edit")]
    public async Task<IActionResult> Edit([FromBody] EditTodoCommand editTodoCommand)
    {
        return Ok(await _mediator.Send(editTodoCommand));
    }

}
