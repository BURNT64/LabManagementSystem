using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.FormCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.FormsAndLinks;

public class CreateModel : PageModel
{
    [BindProperty]
    public FormCategoryModel FormCategory { get; set; }
    
    private readonly IFormCategoryRepository _formCategoryRepository;
    
    public CreateModel(IFormCategoryRepository formCategoryRepository)
    {
        _formCategoryRepository = formCategoryRepository;
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();
        
        var existingFormCategory = await _formCategoryRepository.Select(FormCategory.Name);
        if (existingFormCategory != null)
        {
            ModelState.AddModelError("Database", "Form Category Name has already been used.");
            return Page();
        }
        
        await _formCategoryRepository.Insert(FormCategory);
        
        return RedirectToPage("Index");
    }
}