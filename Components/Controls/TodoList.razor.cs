using Microsoft.AspNetCore.Components;
using blazortodoapp.Models;

namespace BlazorTodoApp.Components.Controls;

public enum TodoItemChangeType
{
    Add,
    Update,
    Remove
}

public class TodoItemChangedEventArgs(TodoItem item, TodoItemChangeType changeType) : EventArgs
{
    public TodoItem Item { get; set; } = item;

    public TodoItemChangeType ChangeType { get; set; } = changeType;
}

public partial class TodoList
{
    [Parameter, EditorRequired]
    public required Dictionary<int, TodoItem> Items { get; set; }

    [Parameter]
    public EventCallback<TodoItemChangedEventArgs> OnChanged { get; set; }

    [Parameter]
    public string Filter { get; set; } = "All";

    [Parameter]
    public RenderFragment<TodoItemTemplateData>? ItemTemplate { get; set; }

    protected TodoItemTemplateData GetTodoItemTemplateData(TodoItem item)
    {
        return new TodoItemTemplateData(item)
        {
            OnChanged = OnItemChanged,
            OnRemoved = OnItemRemoved
        };
    }

    protected void OnItemChanged(TodoItem item)
    {
        Items[item.Id] = item;
        OnChanged.InvokeAsync(new TodoItemChangedEventArgs(item, TodoItemChangeType.Update));
    }
    
    protected void OnItemRemoved(TodoItem item)
    {
        Items.Remove(item.Id);
        OnChanged.InvokeAsync(new TodoItemChangedEventArgs(item, TodoItemChangeType.Remove));
    }   

    protected IEnumerable<KeyValuePair<int, TodoItem>> FilterItems(Dictionary<int, TodoItem> items)
    {
        return Filter switch
        {
            "All" => items,
            "Active" => items.Where(item => !item.Value.IsDone),
            "Completed" => items.Where(item => item.Value.IsDone),
            // Handle unexpected filter value
            _ => [],
        };
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        StateHasChanged();
        //for testing filters
        Console.WriteLine($"Filter received in TodoList: {Filter}");
        Console.WriteLine($"Number of items received in TodoList: {Items.Count}");
    }
}