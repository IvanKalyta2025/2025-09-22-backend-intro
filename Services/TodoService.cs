using System.Text.Json;
using System.Text.Json.Serialization; // Можно удалить, если не используется явно, но не мешает

class TodoList
{
  // Свойства
  public FileStorageService fileStore;

  // Как создать новый экземпляр
  public TodoList(FileStorageService fileStorageService)
  {
    fileStore = fileStorageService;
  }

  // Методы, что мы можем делать с этим объектом
  public List<TodoItem> GetAllTodoes()
  {
    return LoadTodoesFromStorage();
  }

  public TodoItem CreateNewTodo(TodoItemCreateInfo createInfo)
  {

    if (!Enum.IsDefined(typeof(Importance), createInfo.Importance))
      throw new ArgumentException("Недопустимое значение важности.");
    // Используем значение важности напрямую из createInfo
    var newTodoItem = new TodoItem(createInfo.Description, createInfo.Deadline, createInfo.Importance);

    // Сначала загружаем существующие задачи
    var todoItems = LoadTodoesFromStorage();

    // Добавляем новую задачу в список
    todoItems.Add(newTodoItem);

    // Сохраняем новый список
    SaveTodoesToStorage(todoItems);

    return newTodoItem;
  }

  public void DeleteTodo(string todoId)
  {
    // Сначала загружаем данные
    var todoItems = LoadTodoesFromStorage();

    // Изменяем список
    var foundTodo = todoItems
      .Find(todo => todo.Id.ToString() == todoId);

    if (foundTodo != null)
    {
      todoItems.Remove(foundTodo);
    }
    // Сохраняем изменённый список
    SaveTodoesToStorage(todoItems);
  }

  private List<TodoItem> LoadTodoesFromStorage()
  {
    // Читаем из файлового хранилища
    var todoItemsString = fileStore.Load();

    // Проверка на пустую строку
    if (string.IsNullOrWhiteSpace(todoItemsString))
    {
      return new List<TodoItem>();
    }

    // Преобразуем из строки в список TodoItems
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