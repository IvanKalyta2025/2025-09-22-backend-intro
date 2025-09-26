using System.Text.Json;

class TodoList
{
  // Properties
  public FileStorageService fileStore;

  // How to create a new instance
  public TodoList(FileStorageService fileStorageService)
  {
    fileStore = fileStorageService;
  }

  // Methods, what we can do with this thing
  public List<TodoItem> GetAllTodoes()
  {



    return LoadTodoesFromStorage();
  }

  public TodoItem CreateNewTodo(TodoItemCreateInfo createInfo)
  {
    var newTodoItem = new TodoItem(createInfo.Description, createInfo.Deadline);

    // First load existing todoes
    var todoItems = LoadTodoesFromStorage();

    // Add the new todo item to list
    todoItems.Add(newTodoItem);

    // Save new list
    SaveTodoesToStorage(todoItems);

    return newTodoItem;
  }

  public void DeleteTodo(string todoId)
  {
    // First Load in data
    var todoItems = LoadTodoesFromStorage();

    // Modify the list не понятно
    var foundTodo = todoItems
      .Find(todo => todo.Id.ToString() == todoId);

    if (foundTodo != null)
    {
      todoItems.Remove(foundTodo);
    }
    // Store the modified list
    SaveTodoesToStorage(todoItems);
  }

  private List<TodoItem> LoadTodoesFromStorage()
  {
    // Read from file storage
    var todoItemsString = fileStore.Load();

    // проерка на пустоту в дазе строки 
    if (string.IsNullOrWhiteSpace(todoItemsString))
    {
      return new List<TodoItem>();
    }

    // Convert from string to list of TodoItems
    var todoItems = JsonSerializer.Deserialize<List<TodoItem>>(todoItemsString);

    if (todoItems == null)
    {
      return new List<TodoItem>();
    }

    return todoItems;
  }

  private bool SaveTodoesToStorage(List<TodoItem> todoItems)
  {
    var todoItemsString = JsonSerializer.Serialize(todoItems);
    return fileStore.Save(todoItemsString);
  }
}







// public void DeleteCompletetTodo(string todoId)
// {
//   // First Load in data
//   var todoItems = LoadTodoesFromStorage();

//     todoItems.RemoveAll(todo => todo.Id.ToString() ==  todoId);
//   // Store the modified list
//   SaveTodoesToStorage(todoItems);
// }