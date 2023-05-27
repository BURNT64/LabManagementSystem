using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.FormCategories;
using LabManagementSystem.Data.Repositories.FormDocumentationFiles;
using LabManagementSystem.Data.Repositories.FormDocumentationUrls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.FormsAndLinks;

public class UpdateModel : PageModel
{
    public FormCategoryModel FormCategory { get; set; }
    
    private readonly IFormCategoryRepository _formCategoryRepository;
    
    private readonly IFormDocumentationFileRepository _formDocumentationFileRepository;
    
    private readonly IFormDocumentationUrlRepository _formDocumentationUrlRepository;

    public UpdateModel(
        IFormCategoryRepository formCategoryRepository,
        IFormDocumentationFileRepository formDocumentationFileRepository,
        IFormDocumentationUrlRepository formDocumentationUrlRepository
    )
    {
        _formCategoryRepository = formCategoryRepository;
        _formDocumentationFileRepository = formDocumentationFileRepository;
        _formDocumentationUrlRepository = formDocumentationUrlRepository;

    }
    
    public async Task<IActionResult> OnGet(string? categoryName)
    {
        if (categoryName == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("categoryName"));
        }

        var formCategory = await _formCategoryRepository.Select(categoryName);
        if (formCategory == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("form category"));
        }

        FormCategory = formCategory;

        return Page();
    }
    
    public async Task<IActionResult> OnPostDelete(string? categoryName)
    {
        if (categoryName == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("categoryName"));
        }
        
        await _formCategoryRepository.Delete(categoryName);
        
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnGetDownloadFile(int? documentationId)
    {
        if (documentationId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("documentationId"));
        }

        var documentationFile = await _formDocumentationFileRepository.Select((int) documentationId);
        if (documentationFile == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("form documentation file"));
        }
        
        return File(documentationFile.File, documentationFile.ContentType, documentationFile.Name);
    }
    
    public async Task<IActionResult> OnPostDeleteFile(string? categoryName, int? documentationId)
    {
        if (documentationId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("documentationId"));
        }
        
        await _formDocumentationFileRepository.Delete((int) documentationId);
        
        return RedirectToPage("Update", new { categoryName });
    }

    public async Task<IActionResult> OnPostDeleteUrl(string? categoryName, int? documentationId)
    {
        if (documentationId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("documentationId"));
        }
        
        await _formDocumentationUrlRepository.Delete((int) documentationId);
        
        return RedirectToPage("Update", new { categoryName });
    }
}

