using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.FormCategories;
using LabManagementSystem.Data.Repositories.FormDocumentationFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages
{
    public class usefulform_linksModel : PageModel
    {
        private readonly IFormCategoryRepository _formCategoryRepository;
        private readonly IFormDocumentationFileRepository _formFiles;
 

        public ICollection<FormCategoryModel> FormCategories { get; set; }

        public usefulform_linksModel(IFormCategoryRepository formCategoryRepository, IFormDocumentationFileRepository formFiles)
        {
            _formCategoryRepository = formCategoryRepository;
            _formFiles = formFiles;

        }
        public async Task<IActionResult> OnGet()
        {
            FormCategories = await _formCategoryRepository.SelectAll();
            return Page();
        }
        public async Task<IActionResult> OnGetDownloadFile(int Id)
        {
            if (Id == null)
            {
                return BadRequest(ErrorMessages.HttpParameterNotProvided("documentationId"));
            }

            var documentationFile = await _formFiles.Select(Id);
            if (documentationFile == null)
            {
                return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("equipment documentation file"));
            }
        
            return File(documentationFile.File, documentationFile.ContentType, documentationFile.Name);
        }
    }
}