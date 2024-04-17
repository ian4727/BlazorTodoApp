using Microsoft.AspNetCore.Components;
using blazortodoapp.Models;

namespace BlazorTodoApp.Components.Controls;

public partial class TodoFooter
{
    [Parameter]
    public int TotalItems { get; set; }

    [Parameter]
    public int ActiveItems { get; set; }

    [Parameter]
    public int CompletedItems { get; set; }

    [Parameter]
    public EventCallback OnClearCompleted { get; set; }

    [Parameter]
    public required Dictionary<int, TodoItem> Items { get; set; }

    [Parameter]
    public EventCallback<TodoItem> OnItemRemove { get; set; }

    private int GetUncompletedItemsCount()
    {
        return Items.Values.Count(item => !item.IsDone);
    }

    private int GetCompletedItemsCount()
    {
        return Items.Values.Count(item => item.IsDone);
    }

    private async Task HandleClearCompleted()
    {
        var completedItems = Items.Values.Where(item => item.IsDone).ToList();
        foreach (var item in completedItems)
        {
            Items.Remove(item.Id);
            await OnItemRemove.InvokeAsync(item);
        }
    }
}