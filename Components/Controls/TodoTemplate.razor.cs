using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using blazortodoapp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorTodoApp.Components.Controls;

public partial class TodoTemplate<TItem> where TItem : TodoItem
{
    [Parameter, EditorRequired]
    public required TodoItemTemplateData TemplateData { get; set; }

    // [Parameter, AllowNull]
    // public TItem Item { get; set; }
    [Parameter, AllowNull]
    public RenderFragment<TItem> RowTemplate { get; set; }

    [Parameter]
    public string? LayoutType { get; set; }

    // public TodoItem Item
    // {
    //     get => TemplateData.Item;
    //     set => TemplateData.Item = value;
    // }
    public TItem Item
    {
        get => (TItem)TemplateData.Item;
        set => TemplateData.Item = value;
    }

    private void OnTodoItemDbClick()
    {
        Item = Item with { IsEditing = true };
        TemplateData.OnChanged?.Invoke(Item);
    }

    private void OnTodoEditingInput(ChangeEventArgs e)
    {
        Item = Item with { Title = e.Value?.ToString() ?? string.Empty };
    }

    private void OnTodoEditingItemKeyPress(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            Item = Item with { IsEditing = false };
            TemplateData.OnChanged?.Invoke(Item);
        }
    }

    private void OnEditInputBlur()
    {
        Item = Item with { IsEditing = false };
        TemplateData.OnChanged?.Invoke(Item);
    }

    private void OnTodoItemInputCheckChanged(ChangeEventArgs e)
    {
        Item = Item with { IsDone = e.Value?.ToString()?.ToLowerInvariant() == "true" };
        TemplateData.OnChanged?.Invoke(Item);
    }

    private void OnTodoItemRemove()
    {
        TemplateData.OnRemoved?.Invoke(Item);
    }

    private string GetCompletedClass()
    {
        return Item.IsDone ? "completed" : string.Empty;
    }
}

