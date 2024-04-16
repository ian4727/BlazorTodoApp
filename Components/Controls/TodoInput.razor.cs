// namespace BlazorTodoApp.Components.Controls;

// public partial class TodoInput
// {
//     [Parameter]
//     public EventCallback<string> OnAddTodo { get; set; }

//     protected string CurrentTodo { get; set; } = string.Empty;

//     protected void OnTodoInputKeyPress(KeyboardEventArgs e)
//     {
//         if (e.Key == "Enter")
//         {
//             AddTodo();
//         }
//     }

//     protected async void AddTodo()
//     {
//         if (!string.IsNullOrWhiteSpace(CurrentTodo))
//         {
//             await OnAddTodo.InvokeAsync(CurrentTodo);
//             CurrentTodo = string.Empty;
//         }
//     }
// }