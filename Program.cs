var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<FileStorageService>();
builder.Services.AddSingleton<TodoList>();

var app = builder.Build();

// Router definitions (Mappings)

// Read all todoes
app.MapGet("/todo", (TodoList todoList) =>
{
  return todoList.GetAllTodoes();
});

// Create a new todo
app.MapPost("/todo", (TodoList todoList, TodoItemCreateInfo createInfo) =>
{

  return todoList.CreateNewTodo(createInfo);
});

app.MapDelete("/todo/{todoId}", (TodoList todoList, string todoId) =>
{
  todoList.DeleteTodo(todoId);
  return Results.NoContent();
});



app.Run();

