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

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create([FromBody] TodoItem todo)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.AddAsync(todo);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }
}