using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationManager.Data;

[Authorize]
public class RoomListModel : PageModel
{
            private readonly ApplicationDbContext _context;

            public RoomListModel(ApplicationDbContext context)
            {
                _context = context;
                Rooms = new List<Room>();
            }
            
            [BindProperty]
            public Room Room { get; set; }
            public IList<Room> Rooms { get;set; }

            public void OnGet()
            {
                Rooms = _context.Rooms.ToList();
            }
            public IActionResult OnPostDelete(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return RedirectToPage();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var roomInDb = _context.Rooms.Find(Room.Id);
            if (roomInDb == null)
            {
                return NotFound();
            }

            roomInDb.Name = Room.Name;
            roomInDb.Capacity = Room.Capacity;
            _context.SaveChanges();

            return RedirectToPage("/RoomList");
        }
    
}
