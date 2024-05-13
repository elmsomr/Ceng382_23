using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

public static class ReservationService
{
    private static List<Reservation> reservations = new List<Reservation>();

    public static void InitializeReservations(string jsonFilePath)
    {
        try
        {
            string jsonString = File.ReadAllText(jsonFilePath);
            reservations = JsonSerializer.Deserialize<List<Reservation>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Reservation>();
        }
        catch (FileNotFoundException)
        {
            File.WriteAllText("ReservationData.json", "[]");
            reservations = new List<Reservation>();
        }
    }
    private static void PrintReservations(List<Reservation> reservations)
    {
        foreach (var reservation in reservations)
        {
            Console.WriteLine($"DateTime : {reservation.DateTime}, Reserver : {reservation.ReserverName}, Room : {reservation.Room.roomName} , Capacity : {reservation.Room.capacity}\n");
        }
    }

    public static void DisplayReservationByReserver(string name)
    {
        var filteredReservations = filterByName(name);
        Console.WriteLine($"Reservations for {name}\n");
        PrintReservations(filteredReservations);
    }

    private static List<Reservation> filterByName(string name)
    {
        var filteredReservations = reservations.Where(r => r.ReserverName.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        return filteredReservations;
    }

    public static void DisplayReservationByRoomId(string Id)
    {
        var filteredReservations = filterByRoomId(Id);
        Console.WriteLine($"Reservations for RoomId {Id}\n");
        PrintReservations(filteredReservations);
    }

    private static List<Reservation> filterByRoomId(string Id)
    {
        var filteredReservations = reservations.Where(r => r.Room.roomId.Equals(Id, StringComparison.OrdinalIgnoreCase)).ToList();
        return filteredReservations;
    }
}