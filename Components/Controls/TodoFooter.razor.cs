using Microsoft.AspNetCore.Components;
using blazortodoapp.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorTodoApp.Components.Controls;

public partial class TodoFooter
{
    // [Parameter]
    // public int TotalItems { get; set; }

    // [Parameter]
    // public int ActiveItems { get; set; }

    // [Parameter]
    // public int CompletedItems { get; set; }

    // [Parameter]
    // public EventCallback OnClearCompleted { get; set; }

    [Parameter]
    public required Dictionary<int, TodoItem> Items { get; set; }

    [Parameter]
    public EventCallback<TodoItem> OnItemRemove { get; set; }

    [Parameter]
    public string Filter { get; set; } = "All";

    [Parameter]
    public EventCallback<string> OnFilterChanged { get; set; }

    [Parameter]
    public EventCallback<TodoItemChangedEventArgs> OnChanged { get; set; }

    private int GetUncompletedItemsCount()
    {
        return Items.Values.Count(item => !item.IsDone);
    }

    private async Task HandleFilterClick(string filter)
    {
        Filter = filter;
        await OnFilterChanged.InvokeAsync(filter);
    }

    protected async Task ClearCompletedItems()
    {
        var completedItems = Items.Values.Where(item => item.IsDone).ToList();
        
        foreach (var item in completedItems)
        {
            Items.Remove(item.Id);
            await OnItemRemove.InvokeAsync(item);
        }
        // Creating a dummy variable to make sure user don't pass a null value
        var dummyItem = new TodoItem(-1, "Dummy", false, false); 
        await OnChanged.InvokeAsync(new TodoItemChangedEventArgs(dummyItem, TodoItemChangeType.Remove));
    }
}