using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public sealed class JsonLoader
{
  public static List<T> LoadAllFromDirectory<T>(string dir)
  {
    List<T> data = new List<T>();
    foreach (var file in FileSystem.Mounted.FindFile(dir))
    {
      Log.Info(file);
      // Read JSON from the file
      string jsonData = FileSystem.Mounted.ReadAllText(file);
      if( jsonData.Length < 1 || jsonData == null) continue;

      // Deserialize JSON into a list of T objects
      data.Add(JsonSerializer.Deserialize<T>(jsonData));
    }
    return data;
  }
}