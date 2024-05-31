using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationManager.Data;
using System.Threading.Tasks;

namespace RoomManagement.Pages
{
    public class EditRoomModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditRoomModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Room Room { get; set; }

        public IActionResult OnGet(int id)
        {
            Room = _context.Rooms.Find(id);
            if (Room == null)
            {
                return NotFound();
            }
            return Page();
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
}
