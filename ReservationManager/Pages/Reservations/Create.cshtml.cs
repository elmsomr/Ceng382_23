using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservationManager.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CreateModel> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateModel(ApplicationDbContext context, ILogger<CreateModel> logger, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _logger = logger;
        _userManager = userManager;
    }

    [BindProperty]
    public Reservation Reservation { get; set; }
    public IList<Room> Rooms { get; set; }

    public async Task OnGetAsync()
    {
        Rooms = await _context.Rooms.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            _logger.LogError("User not found.");
            return Challenge();
        }

        Reservation.UserId = user.Id;
        Reservation.User = user;
        var room = await _context.Rooms.FindAsync(Reservation.RoomId);
        if (room == null)
        {
            ModelState.AddModelError("Reservation.RoomId", "The selected room does not exist.");
            _logger.LogError("The selected room does not exist.");
        }
        else
        {
            Reservation.Room = room;
        }

        _logger.LogInformation("Reservation state before validation: {@Reservation}", Reservation);

        if (!ModelState.IsValid)
        {
            Rooms = await _context.Rooms.ToListAsync();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    _logger.LogError("Validation error: {Error}", error.ErrorMessage);
                }
            }
            return Page();
        }

        _context.Reservations.Add(Reservation);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Reservation created: {Reservation}", Reservation);

        return RedirectToPage("/Index");
    }
}