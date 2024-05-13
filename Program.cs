using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Program
{
    public static void Main()
    {
        string jsonFilePath = "ReservationData.json";
        ReservationService.InitializeReservations(jsonFilePath);

        Console.WriteLine("----------ReservationService----------\n");
        
        ReservationService.DisplayReservationByReserver("Jane Smith");
        ReservationService.DisplayReservationByRoomId("01");
        
        string logJsonFilePath = "LogData.json";
        LogService.InitializeLogs(logJsonFilePath);
        
        Console.WriteLine("----------LogService----------\n");
        
        Console.WriteLine("With the given username;\n");
        var logs = LogService.DisplayLogsByName("Jane Smith");
        foreach (var log in logs)
        {
            Console.WriteLine($"Timestamp: {log.Timestamp}, ReserverName: {log.ReserverName}, Action: {log.Action}\n");
        }
        
        Console.WriteLine("Between given time intervals;\n");
        var logs2 = LogService.DisplayLogs(DateTime.Parse("2022-01-01"), DateTime.Parse("2024-05-31"));
        foreach (var log in logs2)
        {
            Console.WriteLine($"Timestamp: {log.Timestamp}, ReserverName: {log.ReserverName}, Action: {log.Action}\n");
        }
    }
}