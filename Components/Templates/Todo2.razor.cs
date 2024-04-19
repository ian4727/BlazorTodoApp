using Microsoft.AspNetCore.Components;
using blazortodoapp.Models;

namespace BlazorTodoApp.Components.Templates;

public partial class Todo2
{
    [Parameter, EditorRequired]
    public required TodoItemTemplateData2 TemplateDataV2 { get; set; }

    public TodoItem Item
    {
        get => TemplateDataV2.Item;
        set => TemplateDataV2.Item = value;
    }
}
