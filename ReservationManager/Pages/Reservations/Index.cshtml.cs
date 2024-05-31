using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReservationManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationManager.Pages.Reservations
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservations { get; set; }
        public IList<Room> Rooms { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RoomFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? StartDateFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? EndDateFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? CapacityFilter { get; set; }

        public async Task OnGetAsync()
        {
            Rooms = await _context.Rooms.ToListAsync();

            var reservations = _context.Reservations.Include(r => r.Room).Include(r => r.User).AsQueryable();

            if (!string.IsNullOrEmpty(RoomFilter))
            {
                reservations = reservations.Where(r => r.Room.Name.Contains(RoomFilter));
            }

            if (StartDateFilter.HasValue && EndDateFilter.HasValue)
            {
                reservations = reservations.Where(r => r.StartDate >= StartDateFilter && r.EndDate <= EndDateFilter);
            }

            if (CapacityFilter.HasValue)
            {
                reservations = reservations.Where(r => r.Room.Capacity == CapacityFilter);
            }

            Reservations = await reservations.ToListAsync();
        }
    }
}
