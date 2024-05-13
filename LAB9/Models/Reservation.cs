public class Reservation
{
    public int Id { get; set; }
    public required string ReserverName { get; set; }
    public required string RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}