using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.RoomDocumentationFiles;
using LabManagementSystem.Data.Repositories.RoomDocumentationUrls;
using LabManagementSystem.Data.Repositories.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Rooms.Documentation;

public class IndexModel : PageModel
{
    public RoomModel Room { get; set; } = null!;

    private readonly IRoomRepository _roomRepository;

    private readonly IRoomDocumentationFileRepository _roomDocumentationFileRepository;
    
    private readonly IRoomDocumentationUrlRepository _roomDocumentationUrlRepository;

    public IndexModel(
        IRoomRepository roomRepository,
        IRoomDocumentationFileRepository roomDocumentationFileRepository,
        IRoomDocumentationUrlRepository roomDocumentationUrlRepository
    )
    {
        _roomRepository = roomRepository;
        _roomDocumentationFileRepository = roomDocumentationFileRepository;
        _roomDocumentationUrlRepository = roomDocumentationUrlRepository;
    }
    
    public async Task<IActionResult> OnGet(string? roomCode)
    {
        if (roomCode == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("roomCode"));
        }
        
        var room = await _roomRepository.Select(roomCode);
        if (room == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("room"));
        }

        Room = room;

        return Page();
    }

    public async Task<IActionResult> OnGetDownloadFile(int? documentationId)
    {
        if (documentationId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("documentationId"));
        }

        var documentationFile = await _roomDocumentationFileRepository.Select((int) documentationId);
        if (documentationFile == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("room documentation file"));
        }

        return File(documentationFile.File, documentationFile.ContentType, documentationFile.Name);
    }

    public async Task<IActionResult> OnPostDeleteFile(string? roomCode, int? documentationId)
    {
        if (documentationId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("documentationId"));
        }

        await _roomDocumentationFileRepository.Delete((int) documentationId);
        
        return RedirectToPage("/Admin/Rooms/Documentation/Index", new { roomCode });
    }

    public async Task<IActionResult> OnPostDeleteUrl(string? roomCode, int? documentationId)
    {
        if (documentationId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("documentationId"));
        }

        await _roomDocumentationUrlRepository.Delete((int) documentationId);
        
        return RedirectToPage("/Admin/Rooms/Documentation/Index", new { roomCode });
    }
}