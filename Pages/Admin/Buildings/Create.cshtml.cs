using System.ComponentModel.DataAnnotations;
using LabManagementSystem.API.Files;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Buildings;
using LabManagementSystem.Data.Repositories.Campuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Buildings;

public class CreateModel : PageModel
{
    [BindProperty]
    public BuildingModel Building { get; set; }
    
    [BindProperty]
    [Display(Name = "Image")]
    public IFormFile Image { get; set; }

    public readonly ICampusRepository CampusRepository;

    private readonly IBuildingRepository _buildingRepository;

    public CreateModel(ICampusRepository campusRepository, IBuildingRepository buildingRepository)
    {
        CampusRepository = campusRepository;
        _buildingRepository = buildingRepository;
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Building)}.{nameof(Building.Image)}");
        ModelState.Remove($"{nameof(Building)}.{nameof(Building.Campus)}");
        if (!ModelState.IsValid) return Page();

        BuildingModel? existingBuilding = await _buildingRepository.Select(Building.Name);
        if (existingBuilding != null)
        {   
            ModelState.AddModelError("Database", "A building with the provided name already exists.");
            return Page();
        }

        Building.Image = await FileSerialisationHelper.ConvertFileToBase64(Image);

        await _buildingRepository.Insert(Building);
        
        return RedirectToPage("/Admin/Buildings/Index");
    }
}