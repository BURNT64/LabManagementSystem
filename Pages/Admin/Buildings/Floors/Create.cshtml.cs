using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LabManagementSystem.API.Errors;
using LabManagementSystem.API.Files;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Buildings;
using LabManagementSystem.Data.Repositories.Floors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Buildings.Floors;

public class CreateModel : PageModel
{
    [BindProperty]
    public FloorModel Floor { get; set; } = new();

    [BindProperty]
    [Display(Name = "Map Image")]
    public IFormFile MapImage { get; set; }

    private readonly IFloorRepository _floorRepository;

    public CreateModel(IFloorRepository floorRepository)
    {
        _floorRepository = floorRepository;
    }
    
    public IActionResult OnGet(string? buildingName)
    {
        if (buildingName == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("buildingName"));
        }

        Floor.BuildingName = buildingName;

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Floor)}.{nameof(Floor.Building)}");
        ModelState.Remove($"{nameof(Floor)}.{nameof(Floor.MapImage)}");
        if (!ModelState.IsValid) return Page();

        Floor.MapImage = await FileSerialisationHelper.ConvertFileToBase64(MapImage);

        await _floorRepository.Insert(Floor);

        return RedirectToPage("/Admin/Buildings/Floors/Index", new { buildingName = Floor.BuildingName });
    }
}