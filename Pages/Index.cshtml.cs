using LabManagementSystem.Data.Repositories.Buildings;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages;

public class IndexModel : PageModel
{
    public readonly IBuildingRepository BuildingRepository;

    public IndexModel(IBuildingRepository buildingRepository)
    {
        BuildingRepository = buildingRepository;
    }
}