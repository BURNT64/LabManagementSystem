using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.RoomDocumentationUrls;
using LabManagementSystem.Data.Repositories.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Rooms.Documentation.Urls;

public class CreateModel : PageModel
{
    [BindProperty]
    public RoomDocumentationUrlModel RoomDocumentationUrl { get; set; } = new();
    
    private readonly IRoomDocumentationUrlRepository _roomDocumentationUrlRepository;

    public CreateModel(IRoomDocumentationUrlRepository roomDocumentationUrlRepository)
    {
        _roomDocumentationUrlRepository = roomDocumentationUrlRepository;
    }
    
    public IActionResult OnGet(string? roomCode)
    {
        if (roomCode == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("roomCode"));
        }

        RoomDocumentationUrl.RoomCode = roomCode;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(RoomDocumentationUrl)}.{nameof(RoomDocumentationUrl.Room)}");
        if (!ModelState.IsValid) return Page();

        await _roomDocumentationUrlRepository.Insert(RoomDocumentationUrl);

        return RedirectToPage("/Admin/Rooms/Documentation/Index", new { roomCode = RoomDocumentationUrl.RoomCode });
    }
}