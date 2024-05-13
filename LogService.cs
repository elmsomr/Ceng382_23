using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

public static class LogService
{
    private static List<LogEntry> logs = new List<LogEntry>();
    private const string LogFileName = "LogData.json";

    public static void InitializeLogs(string jsonFilePath)
    {
        try
        {
            string jsonString = File.ReadAllText(jsonFilePath);
            logs = JsonSerializer.Deserialize<List<LogEntry>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<LogEntry>();
        }
        catch (FileNotFoundException)
        {
            File.WriteAllText(LogFileName, "[]");
            logs = new List<LogEntry>();
        }
    }

    public static void WriteLog(LogEntry logEntry)
    {
        logs.Add(logEntry);
        string jsonString = JsonSerializer.Serialize(logs);
        File.WriteAllText(LogFileName, jsonString);
    }

    public static List<LogEntry> DisplayLogsByName(string name)
    {
        return logs.Where(l => l.ReserverName.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public static List<LogEntry> DisplayLogs(DateTime start, DateTime end)
    {
        return logs.Where(l => l.Timestamp >= start && l.Timestamp <= end).ToList();
    }
}

public record LogEntry(DateTime Timestamp, string ReserverName, string Action);

