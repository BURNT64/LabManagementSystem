using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.FormCategories;
using LabManagementSystem.Data.Repositories.FormDocumentationUrls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.FormsAndLinks.Urls;

public class CreateModel : PageModel
{
    [BindProperty]
    public FormDocumentationUrlModel FormDocumentationUrl { get; set; } = new();

    private readonly IFormDocumentationUrlRepository _formDocumentationUrlRepository;
    
    public CreateModel(IFormDocumentationUrlRepository formDocumentationUrlRepository)
    {
        _formDocumentationUrlRepository = formDocumentationUrlRepository;
    }

    public IActionResult OnGet(string? categoryName)
    {
        if (categoryName == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("categoryName"));
        }

        FormDocumentationUrl.CategoryName = categoryName;

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(FormDocumentationUrl)}.{nameof(FormDocumentationUrl.Category)}");
        if (!ModelState.IsValid) return Page();

        await _formDocumentationUrlRepository.Insert(FormDocumentationUrl);
        
        return RedirectToPage("/Admin/FormsAndLinks/Update", new { categoryName = FormDocumentationUrl.CategoryName });
    }
}