namespace blazortodoapp.Models;

//Model to follow everytime a new task item is added
public record TodoItem(int Id, string Title, bool IsEditing, bool IsDone);