using LabManagementSystem.Data.Repositories.FormCategories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.FormsAndLinks;

public class IndexModel : PageModel
{
    public readonly IFormCategoryRepository FormCategoryRepository;
    
    public IndexModel(IFormCategoryRepository formCategoryRepository)
    {
        FormCategoryRepository = formCategoryRepository;
    }
}