using Microsoft.AspNetCore.Components;
using blazortodoapp.Models;
using static System.ValueTuple;

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

    //test code
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

    //test code
    protected IEnumerable<KeyValuePair<int, TodoItem>> FilterItems(Dictionary<int, TodoItem> items)
    {
        if (Filter == "All")
        {
            return items;
        }
        else if (Filter == "Active")
        {
            return items.Where(item => !item.Value.IsDone);
        }
        else if (Filter == "Completed")
        {
            return items.Where(item => item.Value.IsDone);
        }
        else
        {
            // Handle unexpected filter value (optional)
            return Enumerable.Empty<KeyValuePair<int, TodoItem>>();
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        StateHasChanged();
        Console.WriteLine($"Filter received in TodoList: {Filter}");
        Console.WriteLine($"Number of items received in TodoList: {Items.Count}");
    }
}