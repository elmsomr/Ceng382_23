using System.IO;
using System.Text.Json;
public class ReservationRepository : IReservationRepository
{
   private List<Reservation> reservations = new();
   private string _filePath;
   private IFileIOService _fileIOService;

   public ReservationRepository(string filePath, IFileIOService fileIOService)
   {
       _filePath = filePath;
       _fileIOService = fileIOService;
       LoadReservationsFromJson();
   }

   private void LoadReservationsFromJson()
{
    try
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                reservations = new List<Reservation>();
            }
            else
            {
                reservations = JsonSerializer.Deserialize<List<Reservation>>(json) ?? new List<Reservation>();
            }
        }
        else
        {
            Console.WriteLine($"File {_filePath} does not exist. Creating a new reservation list.");
            reservations = new List<Reservation>();
        }
    }
    catch (JsonException ex)
    {
        Console.WriteLine($"JSON error in file {_filePath}: {ex.Message}");
        reservations = new List<Reservation>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while loading reservations: {ex.Message}");
    }
}


   public void SaveDataToJson()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(reservations, options);
            _fileIOService.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving data to JSON: {ex.Message}");
        }
    }

   public void AddReservation(Reservation reservation)
    {
        try
        {
            reservations.Add(reservation);
            SaveDataToJson();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while adding the reservation: {ex.Message}");
        }
}


   public void DeleteReservation(Reservation reservation)
   {
       reservations.Remove(reservation);
       SaveDataToJson();
   }

   public List<Reservation> GetAllReservations()
   {
       return reservations;
   }
}