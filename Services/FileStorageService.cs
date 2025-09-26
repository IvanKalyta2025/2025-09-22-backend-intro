class FileStorageService
{
  private string path = "store.json";

  public bool Save(string content)
  {
    File.WriteAllText(path, content);

    return true;
  }

  public string Load()
  {
    var fileContent = File.ReadAllText(path);
    return fileContent;
  }
}