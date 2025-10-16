using Microsoft.AspNetCore.Mvc;
using TodoApi.Entities;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoService _service;
    public TodoController(TodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> Get(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpGet("incoming")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetIncoming([FromQuery] string period = "today")
    {
        var now = DateTimeOffset.UtcNow;
        DateTimeOffset from = now, to = now;

        switch (period.ToLower())
        {
            case "tomorrow":
                from = now.Date.AddDays(1);
                to = from.AddDays(1).AddTicks(-1);
                break;
            case "week":
                var monday = now.Date.AddDays(-(int)now.UtcDateTime.DayOfWeek + 1);
                from = monday;
                to = from.AddDays(7).AddTicks(-1);
                break;
            default:
                from = now.Date;
                to = from.AddDays(1).AddTicks(-1);
                break;
        }

        var items = await _service.GetIncomingAsync(from, to);
        return Ok(items);
    }


    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create([FromBody] TodoItem todo)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.AddAsync(todo);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }
}