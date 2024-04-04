using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Room
{
    public string? RoomID { get; set; }
    public string? RoomName { get; set; }
    public int Capacity { get; set; }
}

class Reservation
{
    private Room? room;

    public DateTime Time { get; set; }
    public DateTime Date { get; set; }
    public string? ReserverName { get; set; }
    public Room? Room { get => room; set => room = value; }
}

class ReservationHandler
{
    private List<Reservation> reservations;

    public ReservationHandler()
    {
        reservations = new List<Reservation>();
    }

    public void LoadReservationsFromJson(string jsonFile)
    {
        if (File.Exists(jsonFile))
        {
            var json = File.ReadAllText(jsonFile);
            reservations = JsonSerializer.Deserialize<List<Reservation>>(json);
        }
        else
        {
            Console.WriteLine($"File {jsonFile} does not exist. Creating a new reservation list.");
        }
    }

    public void SaveReservationsToJson(string jsonFile)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(reservations, options);
            File.WriteAllText(jsonFile, json);
            Console.WriteLine($"Reservations saved to {jsonFile}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving reservations: {ex.Message}");
        }
}


    public void AddReservation(Reservation reservation)
    {
        reservations.Add(reservation);
        Console.WriteLine($"Reservation added for {reservation?.ReserverName} on {reservation?.Date.ToShortDateString()} at {reservation?.Time.ToShortTimeString()} in room {reservation?.Room?.RoomName}");

    }

    public void DeleteReservation(Reservation reservation)
    {
        if (reservations.Remove(reservation))
        {
            Console.WriteLine($"Reservation deleted for {reservation.ReserverName} on {reservation.Date.ToShortDateString()} at {reservation.Time.ToShortTimeString()} in room {reservation?.Room?.RoomName}");
        }
        else
        {
            Console.WriteLine($"No reservation found for {reservation.Date.ToShortDateString()} at {reservation.Time.ToShortTimeString()} in room {reservation?.Room?.RoomName}");
        }
    }

    public void DisplayWeeklySchedule()
    {
        foreach (var reservation in reservations)
        {
            Console.WriteLine($"{reservation.Date.ToShortDateString()} {reservation.Time.ToShortTimeString()} - {reservation.ReserverName} - {reservation?.Room?.RoomName}");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        var handler = new ReservationHandler();
        handler.LoadReservationsFromJson("Data.json");
        var newReservation1 = new Reservation
        {
            Time = new DateTime(2024, 01, 06, 15, 30, 00),
            Date = new DateTime(2024, 01, 06),
            ReserverName = "Omer",
            Room = new Room { RoomID = "001", RoomName = "A-101", Capacity = 10 }
        };

        var newReservation2 = new Reservation
        {
            Time = new DateTime(2024, 06, 06, 09, 00, 00),
            Date = new DateTime(2024, 06, 06),
            ReserverName = "Sakir",
            Room = new Room { RoomID = "002", RoomName = "A-102", Capacity = 5 }
        };
        
        handler.AddReservation(newReservation1);
        handler.AddReservation(newReservation2);

        handler.DisplayWeeklySchedule();
        
        handler.DeleteReservation(newReservation1);
        handler.DisplayWeeklySchedule();
        
        handler.DeleteReservation(newReservation2);
        handler.DisplayWeeklySchedule();
        
        handler.SaveReservationsToJson("Data.json");
    }
}
