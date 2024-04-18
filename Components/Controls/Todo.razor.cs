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

    //for tracking whether the checkbox is ticked or not
    protected void OnTodoItemInputCheckChanged(ChangeEventArgs e)
    {
        string isCheckedString = e.Value?.ToString()?.ToLowerInvariant() ?? "false";
        Item = Item with { IsDone = bool.Parse(isCheckedString) };
        TemplateData.OnChanged?.Invoke(Item);
    }
    
    //for deleting an item once x is clicked
    protected void OnTodoItemRemove(MouseEventArgs e)
    {
        TemplateData.OnRemoved?.Invoke(Item);
    }

    //For strikethrough effect
    protected string GetCompletedClass()
    {
        return Item.IsDone ? "completed" : string.Empty;
    }
}