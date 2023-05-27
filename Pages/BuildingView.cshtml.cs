using LabManagementSystem.API.Errors;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Buildings;
using Microsoft.AspNetCore.Mvc;

namespace LabManagementSystem.Pages;

public class BuildingViewModel : PageModel
{
    [BindProperty]
    public BuildingModel Building { get; set; }

    private readonly IBuildingRepository _buildingRepository;

    public BuildingViewModel(IBuildingRepository buildingRepository)
    {
        _buildingRepository = buildingRepository;
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
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("building"));;
        }

        Building = building;
        
        return Page();
    }
}