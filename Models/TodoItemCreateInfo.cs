class TodoItemCreateInfo
{
  public required string Description { get; set; }
  public DateTime Deadline { get; set; }
  public string Importance { get; set; } = "Normal";
}
