using System.ComponentModel.DataAnnotations;
using LabManagementSystem.API.Errors;
using LabManagementSystem.API.Files;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.FormCategories;
using LabManagementSystem.Data.Repositories.FormDocumentationFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.FormsAndLinks.Files;

public class CreateModel : PageModel
{
    [BindProperty]
    public FormDocumentationFileModel FormDocumentationFile { get; set; } = new();
    
    [BindProperty]
    [Display(Name = "File")]
    public IFormFile File { get; set; }
    
    private readonly IFormDocumentationFileRepository _formDocumentationFileRepository;
    
    public CreateModel(IFormDocumentationFileRepository formDocumentationFileRepository)
    {
        _formDocumentationFileRepository = formDocumentationFileRepository;
    }

    public IActionResult OnGet(string? categoryName)
    {
        if (categoryName == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("categoryName"));
        }

        FormDocumentationFile.CategoryName = categoryName;

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(FormDocumentationFile)}.{nameof(FormDocumentationFile.File)}");
        ModelState.Remove($"{nameof(FormDocumentationFile)}.{nameof(FormDocumentationFile.Category)}");
        ModelState.Remove($"{nameof(FormDocumentationFile)}.{nameof(FormDocumentationFile.ContentType)}");
        if (!ModelState.IsValid) return Page();

        FormDocumentationFile.File = await FileSerialisationHelper.ConvertFileToBase64(File);
        FormDocumentationFile.ContentType = File.ContentType;
        
        await _formDocumentationFileRepository.Insert(FormDocumentationFile);
        
        return RedirectToPage("/Admin/FormsAndLinks/Update", new { categoryName = FormDocumentationFile.CategoryName });
    }
}