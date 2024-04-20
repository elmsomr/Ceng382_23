using System.Collections.Generic;
using System.Text.Json;

public class FileLogger : ILogger 
{
    private string _filePath;
    private IFileIOService _fileIOService;

    public FileLogger(string filePath, IFileIOService fileIOService)
    {
        _filePath = filePath;
        _fileIOService = fileIOService;
    }

    public void Log(LogRecord log) 
    {
        var existingLogs = new List<LogRecord>();
        if (_fileIOService.Exists(_filePath))
        {
            var json = _fileIOService.ReadAllText(_filePath);
            existingLogs = JsonSerializer.Deserialize<List<LogRecord>>(json) ?? new List<LogRecord>();
        }

        existingLogs.Add(log);

        var options = new JsonSerializerOptions { WriteIndented = true };
        var logsJson = JsonSerializer.Serialize(existingLogs, options);
        _fileIOService.WriteAllText(_filePath, logsJson);
    }
}