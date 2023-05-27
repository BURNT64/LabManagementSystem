using System.ComponentModel.DataAnnotations;
using LabManagementSystem.API.Errors;
using LabManagementSystem.API.Files;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Buildings;
using LabManagementSystem.Data.Repositories.Campuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Buildings;

public class UpdateModel : PageModel
{
    [BindProperty]
    public BuildingModel Building { get; set; }

    [BindProperty]
    [Display(Name = "Image")]
    public IFormFile? Image { get; set; }

    private readonly IBuildingRepository _buildingRepository;

    public readonly ICampusRepository CampusRepository;

    public UpdateModel(IBuildingRepository buildingRepository, ICampusRepository campusRepository)
    {
        _buildingRepository = buildingRepository;
        CampusRepository = campusRepository;
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

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Building)}.{nameof(Building.Image)}");
        ModelState.Remove($"{nameof(Building)}.{nameof(Building.Campus)}");
        if (!ModelState.IsValid) return Page();

        var existingBuilding = await _buildingRepository.Select(Building.Name);
        if (existingBuilding == null)
        {
            ModelState.AddModelError("Database", ErrorMessages.CouldNotRetrieveFromDatabase("building"));
            return Page();
        }

        if (Image != null)
        {
            existingBuilding.Image = await FileSerialisationHelper.ConvertFileToBase64(Image);
        }
        
        existingBuilding.CampusName = Building.CampusName;

        await _buildingRepository.Update(existingBuilding);

        return RedirectToPage("/Admin/Buildings/Index");
    }
}