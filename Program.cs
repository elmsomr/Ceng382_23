using System;

public class Program
{
    static void Main(string[] args)
    {
        IFileIOService fileIOService = new FileIOService();
        ILogger logger = new FileLogger("LogData.json", fileIOService);
        IReservationRepository reservationRepository = new ReservationRepository("ReservationData.json", fileIOService);

        var room1 = new Room("001", "A-101", 10);
        var newReservation1 = new Reservation(
            Time: new DateTime(2024, 01, 06, 15, 30, 00),
            Date: new DateTime(2024, 01, 06),
            ReserverName: "Omer",
            Room: room1
        );

        var room2 = new Room("002", "A-102", 5);
        var newReservation2 = new Reservation(
            Time: new DateTime(2024, 06, 06, 09, 00, 00),
            Date: new DateTime(2024, 06, 06),
            ReserverName: "Sakir",
            Room: room2
        );

        reservationRepository.AddReservation(newReservation1);
        logger.Log(new LogRecord(DateTime.Now, newReservation1.ReserverName, newReservation1.Room.RoomName));

        reservationRepository.AddReservation(newReservation2);
        logger.Log(new LogRecord(DateTime.Now, newReservation2.ReserverName, newReservation2.Room.RoomName));

        reservationRepository.DeleteReservation(newReservation1);
        logger.Log(new LogRecord(DateTime.Now, newReservation1.ReserverName, newReservation1.Room.RoomName));

        //reservationRepository.DeleteReservation(newReservation2);
        //logger.Log(new LogRecord(DateTime.Now, newReservation2.ReserverName, newReservation2.Room.RoomName));
    }
}