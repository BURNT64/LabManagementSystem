using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Chemicals;
using LabManagementSystem.Data.Repositories.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Chemicals;

public class UpdateModel : PageModel
{
    [BindProperty]
    public ChemicalModel Chemical { get; set; }
    
    private readonly IChemicalRepository _chemicalRepository;

    public readonly IRoomRepository RoomRepository;

    public UpdateModel(IChemicalRepository chemicalRepository, IRoomRepository roomRepository)
    {
        _chemicalRepository = chemicalRepository;
        RoomRepository = roomRepository;
    }
    
    public async Task<IActionResult> OnGet(int? chemicalId)
    {
        if (chemicalId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("chemicalId"));
        }

        var chemical = await _chemicalRepository.Select((int) chemicalId);
        if (chemical == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("chemical"));
        }

        Chemical = chemical;

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove(nameof(Chemical) + "." + nameof(Chemical.LocationRoom));
        if (!ModelState.IsValid) return Page();
        
        await _chemicalRepository.Update(Chemical);
        
        return RedirectToPage("/Chemical/Index");

    }
    
    public async Task<IActionResult> OnPostDelete(int? chemicalId)
    {
        if (chemicalId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("chemicalId"));
        }

        await _chemicalRepository.Delete((int) chemicalId);
        
        return RedirectToPage("/Chemicals/Index");
    }
}