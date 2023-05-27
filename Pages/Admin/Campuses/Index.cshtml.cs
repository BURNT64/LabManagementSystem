using LabManagementSystem.Data.Repositories.Campuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Campuses;

public class IndexModel : PageModel
{
    public readonly ICampusRepository CampusRepository; 
    
    public IndexModel(ICampusRepository campusRepository)
    {
        CampusRepository = campusRepository;
    }
    
    public async Task<IActionResult> OnPostDelete(string campusName)
    {
        await CampusRepository.Delete(campusName);
        return Page();
    }
}
