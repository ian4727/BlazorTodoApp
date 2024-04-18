using Microsoft.AspNetCore.Components;
using blazortodoapp.Models;

namespace BlazorTodoApp.Components.Templates;

public partial class Todo2
{
    [Parameter, EditorRequired]
    public required TodoItemTemplateData2 TemplateDataV2 { get; set; }

    private bool IsMouseOver { get; set; }

    private void ShowDeleteButton()
    {
        IsMouseOver = true;
    }

    private void HideDeleteButton()
    {
        IsMouseOver = false;
    }

    protected void OnTodoItemRemove()
    {
        TemplateDataV2.OnRemoved?.Invoke(TemplateDataV2.Item);
    }
}
