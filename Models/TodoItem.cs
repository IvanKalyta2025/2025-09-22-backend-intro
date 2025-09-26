using System.IO.Compression;

class TodoItem
{
  // Properties / Egenskaper
  public Guid Id { get; }
  public string Description { get; set; }
  public bool IsComplete { get; set; }
  public DateTime Deadline { get; set; }
  public DateTime CreatedAt { get; set; }


  // Constructor / Beskrivelse av hvordan lage denne klasse
  public TodoItem(string description, DateTime deadline)
  {
    Deadline = deadline;
    IsComplete = false;
    CreatedAt = DateTime.Now;
    Id = Guid.NewGuid();

    // We can setup rules for what's allowed here
    if (description.Length < 3 || description.Length < 200)
    {
      // This crashes this part of the program if we use an invalid description
      throw new ArgumentException();
    }

    Description = description;
  }

}