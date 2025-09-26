using System.IO.Compression;
using System.Text;

public enum Importance
{
  Low,
  Normal,
  High,
  Critical
}

class TodoItem
{
  public Guid Id { get; }
  public string Description { get; set; }
  public bool IsComplete { get; set; }
  public DateTime Deadline { get; set; }
  public DateTime CreatedAt { get; set; }
  public Importance Importance { get; set; }

  // 1. Конструктор по умолчанию для десериализации (если нужен)
  public TodoItem()
  {
    Description = string.Empty;
  }


  public TodoItem(string description, DateTime deadline, Importance importance)
  {
    if (!Enum.IsDefined(typeof(Importance), importance))
    {
      throw new ArgumentException("Недопустиме значення важливості.");
    }
    // Исправленная логика валидации
    if (description.Length < 3 || description.Length > 200)
    {
      throw new ArgumentException("Описание должно быть длиной от 3 до 200 символов.");
    }

    if (deadline <= DateTime.Now)
    {
      throw new ArgumentException("Срок выполнения не может быть в прошлом.");
    }

    Importance = importance;
    IsComplete = false;
    CreatedAt = DateTime.Now;
    Id = Guid.NewGuid();
    Description = description;
    Deadline = deadline;

  }
}