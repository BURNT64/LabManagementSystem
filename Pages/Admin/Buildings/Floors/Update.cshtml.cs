using System.ComponentModel.DataAnnotations;
using LabManagementSystem.API.Errors;
using LabManagementSystem.API.Files;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Floors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Buildings.Floors;

public class UpdateModel : PageModel
{
    [BindProperty]
    public FloorModel Floor { get; set; }
    
    [BindProperty]
    [Display(Name = "Map Image")]
    public IFormFile? MapImage { get; set; }

    private readonly IFloorRepository _floorRepository;

    public UpdateModel(IFloorRepository floorRepository)
    {
        _floorRepository = floorRepository;
    }
    
    public async Task<IActionResult> OnGet(int? floorId)
    {
        if (floorId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("floorId"));
        }
        
        var floor = await _floorRepository.Select((int) floorId);
        if (floor == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("floor"));
        }

        Floor = floor;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Floor)}.{nameof(Floor.Building)}");
        ModelState.Remove($"{nameof(Floor)}.{nameof(Floor.MapImage)}");
        if (!ModelState.IsValid) return Page();

        var existingFloor = await _floorRepository.Select(Floor.Id);
        if (existingFloor == null)
        {
            ModelState.AddModelError("Database", ErrorMessages.CouldNotRetrieveFromDatabase("floor"));
            return Page();
        }
        
        if (MapImage != null)
        {
            existingFloor.MapImage = await FileSerialisationHelper.ConvertFileToBase64(MapImage);
        }
        
        existingFloor.Name = Floor.Name;
        existingFloor.BuildingName = Floor.BuildingName;

        await _floorRepository.Update(existingFloor);

        return RedirectToPage("/Admin/Buildings/Floors/Index", new { buildingName = Floor.BuildingName });
    }
}