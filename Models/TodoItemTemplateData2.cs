namespace blazortodoapp.Models;

public class TodoItemTemplateData2(TodoItem item)
{
    public TodoItem Item { get; set; } = item;
    public Action<TodoItem>? OnChanged { get; set; }
    public Action<TodoItem>? OnRemoved { get; set; }
}
