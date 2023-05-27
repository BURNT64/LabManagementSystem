using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Chemicals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Chemicals;

public class ReadModel : PageModel
{
    public ChemicalModel Chemical { get; set; }
    
    private readonly IChemicalRepository _chemicalRepository;
        
    public readonly IActiveDirectoryUserRepository ActiveDirectoryUserRepository;
        
    public ReadModel(
        IChemicalRepository chemicalRepository,
        IActiveDirectoryUserRepository activeDirectoryUserRepository
    )
    {
        _chemicalRepository = chemicalRepository;
        ActiveDirectoryUserRepository = activeDirectoryUserRepository;
    }

    public async Task<IActionResult> OnGetAsync(int? chemicalId)
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
}