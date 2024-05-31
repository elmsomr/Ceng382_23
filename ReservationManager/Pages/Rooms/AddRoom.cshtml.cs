using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationManager.Data;
using System.Threading.Tasks;

[Authorize]
public class AddRoomModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public AddRoomModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Room Room { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Rooms.Add(Room);
        await _context.SaveChangesAsync();
        return RedirectToPage("./RoomList");
    }
}
