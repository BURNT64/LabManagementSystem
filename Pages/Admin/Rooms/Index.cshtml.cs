using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Floors;
using LabManagementSystem.Data.Repositories.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Rooms;

public class IndexModel : PageModel
{
    public readonly IRoomRepository RoomRepository;
    
    public IndexModel(IRoomRepository roomRepository)
    {
        RoomRepository = roomRepository;
    }

    public async Task<IActionResult> OnPostDelete(string? roomCode)
    {
        if (roomCode == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("roomCode"));
        }

        await RoomRepository.Delete(roomCode);

        return Page();
    }
}