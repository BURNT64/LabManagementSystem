using LabManagementSystem.API.Errors;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabManagementSystem.Data.Repositories.Buildings;
using Microsoft.AspNetCore.Mvc;

namespace LabManagementSystem.Pages.Admin.Buildings;

public class IndexModel : PageModel
{
    public readonly IBuildingRepository BuildingRepository;
    
    public IndexModel(IBuildingRepository buildingRepository)
    {
        BuildingRepository = buildingRepository;
    }
    
    public async Task<IActionResult> OnPostDelete(string? buildingName)
    {
        if (buildingName == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("buildingName"));
        }

        await BuildingRepository.Delete(buildingName);

        return Page();
    }
}