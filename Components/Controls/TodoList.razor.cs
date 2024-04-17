using Microsoft.AspNetCore.Components;
using blazortodoapp.Models;
using Microsoft.AspNetCore.Components.Web;

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
}