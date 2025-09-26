// По сути, это JSON-объект, который клиент (фронтенд)
// должен нам отправить
class TodoItemCreateInfo
{
  public required string Description { get; set; }
  public DateTime Deadline { get; set; }
  public Importance Importance { get; set; }
}