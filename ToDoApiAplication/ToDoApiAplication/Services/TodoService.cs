using TodoApi.Entities;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService
{
    private readonly ITodoRepository _repo;

    public TodoService(ITodoRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<TodoItem>> GetAllAsync() => _repo.GetAllAsync();

    public Task<TodoItem?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public Task<IEnumerable<TodoItem>> GetIncomingAsync(DateTimeOffset from, DateTimeOffset to)
        => _repo.GetIncomingAsync(from, to);

    public async Task<TodoItem> AddAsync(TodoItem item)
    {
        if (item.DueAt <= DateTimeOffset.UtcNow)
        {
            item.DueAt = DateTimeOffset.UtcNow;
        }

        return await _repo.AddAsync(item);
    }

    public async Task<bool> UpdateAsync(Guid id, TodoItem updated)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return false;

        existing.Title = updated.Title;
        existing.Description = updated.Description;
        existing.DueAt = updated.DueAt;
        existing.PercentComplete = updated.PercentComplete;
        existing.IsDone = updated.PercentComplete >= 100;
        if (existing.IsDone) existing.CompletedAt = DateTimeOffset.UtcNow;

        await _repo.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> UpdatePercentAsync(Guid id, int percent)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return false;

        existing.PercentComplete = percent;
        existing.IsDone = percent >= 100;
        if (existing.IsDone) existing.CompletedAt = DateTimeOffset.UtcNow;

        await _repo.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> MarkDoneAsync(Guid id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return false;

        existing.IsDone = true;
        existing.PercentComplete = 100;
        existing.CompletedAt = DateTimeOffset.UtcNow;

        await _repo.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return false;

        await _repo.DeleteAsync(existing);
        return true;
    }
}
