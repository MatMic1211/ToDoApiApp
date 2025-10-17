using TodoApi.Entities;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<IEnumerable<TodoItem>> GetIncomingAsync(DateTimeOffset from, DateTimeOffset to);
    Task<TodoItem> AddAsync(TodoItem item);
    Task<bool> UpdateAsync(TodoItem updated);
    Task<bool> UpdatePercentAsync(int id, int percent);
    Task<bool> MarkDoneAsync(int id);
    Task<bool> DeleteAsync(int id);
}
