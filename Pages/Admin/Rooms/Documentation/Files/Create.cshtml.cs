using System.ComponentModel.DataAnnotations;
using LabManagementSystem.API.Errors;
using LabManagementSystem.API.Files;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.RoomDocumentationFiles;
using LabManagementSystem.Data.Repositories.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;

namespace LabManagementSystem.Pages.Admin.Rooms.Documentation.Files;

public class CreateModel : PageModel
{
    [BindProperty]
    public RoomDocumentationFileModel RoomDocumentationFile { get; set; } = new();

    [BindProperty]
    [Display(Name = "File")]
    public IFormFile File { get; set; }
    
    private readonly IRoomDocumentationFileRepository _roomDocumentationFileRepository;

    public CreateModel(IRoomDocumentationFileRepository roomDocumentationFileRepository)
    {
        _roomDocumentationFileRepository = roomDocumentationFileRepository;
    }
    
    public IActionResult OnGet(string? roomCode)
    {
        if (roomCode == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("roomCode"));
        }

        RoomDocumentationFile.RoomCode = roomCode;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(RoomDocumentationFile)}.{nameof(RoomDocumentationFile.File)}");
        ModelState.Remove($"{nameof(RoomDocumentationFile)}.{nameof(RoomDocumentationFile.Room)}");
        ModelState.Remove($"{nameof(RoomDocumentationFile)}.{nameof(RoomDocumentationFile.ContentType)}");
        if (!ModelState.IsValid) return Page();

        RoomDocumentationFile.File = await FileSerialisationHelper.ConvertFileToBase64(File);
        RoomDocumentationFile.ContentType = File.ContentType;

        await _roomDocumentationFileRepository.Insert(RoomDocumentationFile);

        return RedirectToPage("/Admin/Rooms/Documentation/Index", new { roomCode = RoomDocumentationFile.RoomCode });
    }
}