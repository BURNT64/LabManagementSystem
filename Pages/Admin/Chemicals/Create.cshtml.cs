using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Chemicals;
using LabManagementSystem.Data.Repositories.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Chemicals
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ChemicalModel Chemical { get; set; }
        
        private readonly IChemicalRepository _chemicalRepository;
        
        public readonly IRoomRepository RoomRepository;
        
        public CreateModel(IChemicalRepository chemicalRepository, IRoomRepository roomRepository)
        {
            _chemicalRepository = chemicalRepository;
            RoomRepository = roomRepository;
        }
        
        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove(nameof(Chemical) + "." + nameof(Chemical.LocationRoom));
            ModelState.Remove(nameof(Chemical) + "." + nameof(Chemical.OrderedByObjectId));
            if (!ModelState.IsValid) return Page();

            Chemical.OrderedByObjectId = ActiveDirectoryUserRepository.GetUserObjectId(User);

            await _chemicalRepository.Insert(Chemical);

            return RedirectToPage("/Chemicals/Index");
        }
    }
}