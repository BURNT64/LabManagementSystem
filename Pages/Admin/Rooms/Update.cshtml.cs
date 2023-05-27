using System.ComponentModel.DataAnnotations;
using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Floors;
using LabManagementSystem.Data.Repositories.RoomMapHotspots;
using LabManagementSystem.Data.Repositories.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Rooms;

public class UpdateModel : PageModel
{
    [BindProperty]
    public RoomModel Room { get; set; }
    
    [BindProperty]
    public RoomMapHotspotModel RoomMapHotspot { get; set; }

    private readonly IRoomRepository _roomRepository;

    private readonly IRoomMapHotspotRepository _roomMapHotspotRepository;

    public readonly IFloorRepository FloorRepository;

    public UpdateModel(
        IRoomRepository roomRepository,
        IRoomMapHotspotRepository roomMapHotspotRepository,
        IFloorRepository floorRepository
    )
    {
        _roomRepository = roomRepository;
        _roomMapHotspotRepository = roomMapHotspotRepository;
        FloorRepository = floorRepository;
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

        var roomMapHotspot = await _roomMapHotspotRepository.Select(roomCode);
        if (roomMapHotspot == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("roomMapHotspot"));
        }

        Room = room;
        RoomMapHotspot = roomMapHotspot;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Room)}.{nameof(Room.Floor)}");
        ModelState.Remove($"{nameof(Room)}.{nameof(Room.MapHotspot)}");
        ModelState.Remove($"{nameof(RoomMapHotspot)}.{nameof(RoomMapHotspot.Room)}");
        if (!ModelState.IsValid) return Page();

        await _roomRepository.Update(Room);
        await _roomMapHotspotRepository.Update(RoomMapHotspot);
        
        return RedirectToPage("/Admin/Rooms/Index");
    }
}