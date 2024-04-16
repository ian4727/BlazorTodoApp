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
    protected string CurrentTodo { get; set; } = string.Empty;
    protected string CurrentEditingTodo { get; set; } = string.Empty;
    
    protected void OnTodoInputKeyPress(KeyboardEventArgs e)
    {
        if(e.Key == "Enter")
        {
            int newId = Items.Count > 0 ? Items.Keys.Max() + 1 : 1;
            TodoItem todoItem = new(newId, CurrentTodo, false, false);
            Items.Add(newId, todoItem);
            CurrentTodo = string.Empty;
            OnChanged.InvokeAsync(new TodoItemChangedEventArgs(todoItem, TodoItemChangeType.Add));
        }
    }

    protected void OnTodoItemDbClick(TodoItem item)
    {
        Items.Keys.ToList().ForEach(key => Items[key] = Items[key] with { IsEditing = false });
        Items[item.Id] = item with { IsEditing = true };
        
    }

    protected void OnTodoEditingInput(ChangeEventArgs e, TodoItem item)
    {
        Items[item.Id] = item with {Title = e.Value?.ToString() ?? string.Empty};
    }

    protected void OnTodoEditingItemKeyPress(KeyboardEventArgs e, TodoItem item)
    {
        if(e.Key == "Enter")
        {
            Items[item.Id] = item with { IsEditing = false};
            OnChanged.InvokeAsync(new TodoItemChangedEventArgs(Items[item.Id], TodoItemChangeType.Update));
        }
    }

    protected void OnTodoItemInputCheckChanged(ChangeEventArgs e, TodoItem item)
    {
        string isCheckedString = e.Value?.ToString()?.ToLowerInvariant() ?? "false";
        Items[item.Id] = item with { IsDone = bool.Parse(isCheckedString)};
        OnChanged.InvokeAsync(new TodoItemChangedEventArgs(Items[item.Id], TodoItemChangeType.Update));
    }

    protected void OnTodoItemRemove(TodoItem item)
    {
        Items.Remove(item.Id);
        OnChanged.InvokeAsync(new TodoItemChangedEventArgs(item, TodoItemChangeType.Remove));
    }
}