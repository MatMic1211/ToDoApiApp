using TodoApi.Entities;

namespace TodoApi.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<TodoItem>> GetIncomingAsync(DateTimeOffset from, DateTimeOffset to);
    Task<TodoItem> AddAsync(TodoItem todo);
    Task UpdateAsync(TodoItem todo);
    Task DeleteAsync(TodoItem todo);
    Task SaveChangesAsync();
}
