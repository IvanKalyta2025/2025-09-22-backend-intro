var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<FileStorageService>();
builder.Services.AddSingleton<TodoList>();

var app = builder.Build();

// Server static files
app.UseDefaultFiles();
app.UseStaticFiles();

// Router definitions (Mappings)

// Read all todoes
app.MapGet("/todo", (TodoList todoList) =>
{
  return Results.Ok(todoList.GetAllTodoes());
});

// Create a new todo
app.MapPost("/todo", (TodoList todoList, TodoItemCreateInfo createInfo) =>
{
  var newTodoItem = todoList.CreateNewTodo(createInfo);

  return Results.Created($"/todo/{newTodoItem.Id}", newTodoItem);
});

app.MapDelete("/todo/{todoId}", (TodoList todoList, string todoId) =>
{
  todoList.DeleteTodo(todoId);
  return Results.Ok();
});

app.Run();
