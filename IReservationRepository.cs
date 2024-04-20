// IReservationRepository.cs
using System.Collections.Generic;

public interface IReservationRepository 
{
   void AddReservation(Reservation reservation);
   void DeleteReservation(Reservation reservation);
   List<Reservation> GetAllReservations();
}