using System.ComponentModel.DataAnnotations;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Campuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Campuses
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CampusModel Campus { get; set; }
        
        private readonly ICampusRepository _campusRepository;

        public CreateModel(ICampusRepository campusRepository)
        {
            _campusRepository = campusRepository;
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var existingCampus = await _campusRepository.Select(Campus.Name);
            if (existingCampus != null)
            {
                ModelState.AddModelError(string.Empty, "A campus with that name already exists.");
                return Page();
            }

            await _campusRepository.Insert(Campus);

            return RedirectToPage("/Admin/Campuses/Index");
        }
    }
}
