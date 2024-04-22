using System.Reflection.Metadata;
using blazortodoapp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorTodoApp.Components.Controls;

public partial class Todo
{
    [Parameter, EditorRequired]
    public required TodoItemTemplateData TemplateData { get; set; }

    public TodoItem Item
    {
        get => TemplateData.Item;
        set => TemplateData.Item = value;
    }

    //for enabling editing function when todo item is double-clicked
    protected void OnTodoItemDbClick(MouseEventArgs e)
    {
        Item = Item with { IsEditing = true };
        TemplateData.OnChanged?.Invoke(Item);
    }

    //For enabling changing the value to edit
    protected void OnTodoEditingInput(ChangeEventArgs e)
    {
        Item = Item with { Title = e.Value?.ToString() ?? string.Empty };
    }

    //for saving the updated value once enter key is pressed
    protected void OnTodoEditingItemKeyPress(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            Item = Item with { IsEditing = false };
            TemplateData.OnChanged?.Invoke(Item);
        }
    }

    //for making the todo item revert back to default once there has been a click somewhere else
    protected void OnEditInputBlur(FocusEventArgs e)
    {
        TodoItem todoItem = Item with { IsEditing = false };
        Item = todoItem;
        TemplateData.OnChanged?.Invoke(Item);
    }

    protected string GetCheckboxBorderClass() => Item.IsDone ? "border-green-500" : "border-gray-300";

    //for tracking whether the checkbox is ticked or not
    protected void OnToggleTodoItem()
    {
        Item = Item with { IsDone = !Item.IsDone };
        TemplateData.OnChanged?.Invoke(Item);
        Console.WriteLine("This item is clicked");
    }

    //for deleting an item once x is clicked
    protected void OnTodoItemRemove(MouseEventArgs e)
    {
        TemplateData.OnRemoved?.Invoke(Item);
    }
}