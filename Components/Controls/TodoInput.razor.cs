using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorTodoApp.Components.Controls;

public partial class TodoInput
{
    [Parameter]
    public EventCallback<string> OnSave { get; set; } 

    public string CurrentTodo { get; set; } = string.Empty;
    
    protected async Task OnTodoInputKeyPress(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            await OnSave.InvokeAsync(CurrentTodo);
            CurrentTodo = string.Empty;
        }
    }
}