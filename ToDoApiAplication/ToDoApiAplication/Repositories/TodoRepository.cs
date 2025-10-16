using Microsoft.EntityFrameworkCore;
using System;
using TodoApi.Data;
using TodoApi.Entities;

namespace TodoApi.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoContext _db;
    public TodoRepository(TodoContext db) => _db = db;

    public async Task<IEnumerable<TodoItem>> GetAllAsync() =>
        await _db.Todos.AsNoTracking().OrderBy(t => t.DueAt).ToListAsync();

    public async Task<TodoItem?> GetByIdAsync(int id) =>
        await _db.Todos.FindAsync(id);

    public async Task<IEnumerable<TodoItem>> GetIncomingAsync(DateTimeOffset from, DateTimeOffset to) =>
        await _db.Todos.AsNoTracking()
                       .Where(t => t.DueAt >= from && t.DueAt <= to)
                       .OrderBy(t => t.DueAt)
                       .ToListAsync();

    public async Task<TodoItem> AddAsync(TodoItem todo)
    {
        _db.Todos.Add(todo);
        await _db.SaveChangesAsync();
        return todo;
    }

    public async Task UpdateAsync(TodoItem todo)
    {
        _db.Todos.Update(todo);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoItem todo)
    {
        _db.Todos.Remove(todo);
        await _db.SaveChangesAsync();
    }

    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}
