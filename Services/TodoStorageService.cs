using System.Text.Json;
using Microsoft.JSInterop;
using blazortodoapp.Models;

namespace blazortodoapp.Services;

public class TodoStorageService(IJSRuntime jSRuntime)
{
    public async Task SaveTodosAsync(Dictionary<int, TodoItem> items)
    {
        await jSRuntime.InvokeVoidAsync("localStorage.setItem", "todos", JsonSerializer.Serialize(items));
    }

    public async Task<Dictionary<int, TodoItem>> GetTodoAsync()
    {
        string? todos = await jSRuntime.InvokeAsync<string?>("localStorage.getItem", "todos");
        if(string.IsNullOrWhiteSpace(todos)) return [];
        return JsonSerializer.Deserialize<Dictionary<int, TodoItem>>(todos) ?? [];
    }
}