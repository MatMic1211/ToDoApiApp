using TodoApi.Entities;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<IEnumerable<TodoItem>> GetIncomingAsync(DateTimeOffset from, DateTimeOffset to);
    Task<TodoItem> AddAsync(TodoItem todo);
    Task UpdateAsync(TodoItem todo);
    Task DeleteAsync(TodoItem todo);
    Task SaveChangesAsync();
}
