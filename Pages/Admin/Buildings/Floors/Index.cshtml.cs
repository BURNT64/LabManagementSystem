using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Buildings;
using LabManagementSystem.Data.Repositories.Floors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Buildings.Floors;

public class IndexModel : PageModel
{
    public BuildingModel Building { get; set; } = null!;

    private IBuildingRepository _buildingRepository;

    private IFloorRepository _floorRepository;

    public IndexModel(IBuildingRepository buildingRepository, IFloorRepository floorRepository)
    {
        _buildingRepository = buildingRepository;
        _floorRepository = floorRepository;
    }

    public async Task<IActionResult> OnGet(string? buildingName)
    {
        if (buildingName == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("buildingName"));
        }
        
        var building = await _buildingRepository.Select(buildingName);
        if (building == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("building"));
        }

        Building = building;

        return Page();
    }

    public async Task<IActionResult> OnPostDelete(string? buildingName, int? floorId)
    {
        if (floorId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("floorId"));
        }

        await _floorRepository.Delete((int) floorId);

        return RedirectToPage("/Admin/Buildings/Floors/Index", new { buildingName });
    }
}