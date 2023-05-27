using System.ComponentModel.DataAnnotations;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Floors;
using LabManagementSystem.Data.Repositories.RoomMapHotspots;
using LabManagementSystem.Data.Repositories.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Rooms;

public class CreateModel : PageModel
{
    [BindProperty]
    public RoomModel Room { get; set; }
    
    [BindProperty]
    public RoomMapHotspotModel RoomMapHotspot { get; set; }

    private readonly IRoomRepository _roomRepository;
    
    private readonly IRoomMapHotspotRepository _roomMapHotspotRepository;

    public readonly IFloorRepository FloorRepository;

    public CreateModel(
        IRoomRepository roomRepository,
        IRoomMapHotspotRepository roomMapHotspotRepository,
        IFloorRepository floorRepository
    )
    {
        _roomRepository = roomRepository;
        _roomMapHotspotRepository = roomMapHotspotRepository;
        FloorRepository = floorRepository;
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Room)}.{nameof(Room.Floor)}");
        ModelState.Remove($"{nameof(Room)}.{nameof(Room.MapHotspot)}");
        ModelState.Remove($"{nameof(RoomMapHotspot)}.{nameof(RoomMapHotspot.Room)}");
        ModelState.Remove($"{nameof(RoomMapHotspot)}.{nameof(RoomMapHotspot.RoomCode)}");
        if (!ModelState.IsValid) return Page();

        var existingRoom = await _roomRepository.Select(Room.Code);
        if (existingRoom != null)
        {
            ModelState.AddModelError("Database", "Room Room is already used.");
            return Page();
        }

        RoomMapHotspot.RoomCode = Room.Code;

        await _roomRepository.Insert(Room);
        await _roomMapHotspotRepository.Insert(RoomMapHotspot);
        
        return RedirectToPage("/Admin/Rooms/Index");
    }
}