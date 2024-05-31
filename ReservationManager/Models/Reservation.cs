using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class Reservation
{
    public int Id { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int RoomId { get; set; }
    public Room Room { get; set; }

    [Required]
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
}