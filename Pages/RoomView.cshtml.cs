using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabManagementSystem.Data.Repositories.Rooms;
using LabManagementSystem.Data.Models;
using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Repositories.RoomDocumentationFiles;

namespace LabManagementSystem.Pages
{
    public class RoomViewModel : PageModel
    {
        public RoomModel Room { get; set; }

        private readonly IRoomRepository _roomRepository;

        private readonly IRoomDocumentationFileRepository _roomDocumentationFileRepository;

        public RoomViewModel(
            IRoomRepository roomRepository,
            IRoomDocumentationFileRepository roomDocumentationFileRepository
        )
        {
            _roomRepository = roomRepository;
            _roomDocumentationFileRepository = roomDocumentationFileRepository;
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
    }
}
