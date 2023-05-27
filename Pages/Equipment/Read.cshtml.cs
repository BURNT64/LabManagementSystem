using LabManagementSystem.Data.Repositories.Equipment;
using LabManagementSystem.Data.Repositories.LogbookEntries;
using LabManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using Microsoft.AspNetCore.Authorization;

namespace LabManagementSystem.Pages.Equipment;

[Authorize]
public class ReadModel : PageModel
{
    public EquipmentModel Equipment { get; set; }
    
    [BindProperty]
    public LogbookEntryModel LogbookEntry { get; set; }

    private readonly IEquipmentRepository _equipmentRepository;

    public readonly IActiveDirectoryUserRepository ActiveDirectoryUserRepository;

    private readonly ILogbookEntryRepository _logbookEntryRepository;
    
    public ReadModel(
        IEquipmentRepository equipmentRepository,
        IActiveDirectoryUserRepository activeDirectoryUserRepository,
        ILogbookEntryRepository logbookEntryRepository
    )
    {
        _equipmentRepository = equipmentRepository;
        ActiveDirectoryUserRepository = activeDirectoryUserRepository;
        _logbookEntryRepository = logbookEntryRepository;
    }
    
    public async Task<IActionResult> OnGet(int? equipmentId)
    {
        if (equipmentId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("equipmentId"));
        }
        
        var equipment = await _equipmentRepository.Select((int) equipmentId);
        if (equipment == null)
        {
            ModelState.AddModelError(string.Empty, ErrorMessages.CouldNotRetrieveFromDatabase("equipment"));
            return Page();
        }

        Equipment = equipment;
        
        return Page();
    }
        
    public async Task<IActionResult> OnGetDownloadFile(int? equipmentId)
    {
        if (equipmentId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("equipmentId"));
        }
        
        var equipment = await _equipmentRepository.Select((int) equipmentId);
        if (equipment == null)
        {
            ModelState.AddModelError(string.Empty, ErrorMessages.CouldNotRetrieveFromDatabase("equipment"));
            return Page();
        }
        
        if (equipment.DocumentationFile == null)
        {
            return NotFound(ErrorMessages.NoFileToDownload());
        }
        
        return File(equipment.DocumentationFile, equipment.DocumentationFileContentType!, "Documentation");
    }
    
    public async Task<IActionResult> OnPost(int? equipmentId)
    {
        if (equipmentId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("equipmentId"));
        }

        ModelState.Remove($"{nameof(LogbookEntry)}.{nameof(LogbookEntry.Equipment)}");
        ModelState.Remove($"{nameof(LogbookEntry)}.{nameof(LogbookEntry.UserObjectId)}");
        if (!ModelState.IsValid) return RedirectToPage("Read", new { equipmentId});
        
        LogbookEntry.Date = DateTime.Now;
        LogbookEntry.UserObjectId = LabManagementSystem.API.ActiveDirectory.ActiveDirectoryUserRepository.GetUserObjectId(User);
        LogbookEntry.EquipmentId = (int) equipmentId;
        
        await _logbookEntryRepository.Insert(LogbookEntry);
        
        return RedirectToPage("Read", new { equipmentId });
    }

    public async Task<IActionResult> OnPostDeleteLogbookEntry(int? equipmentId, int? logbookEntryId)
    {
        if (equipmentId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("equipmentId"));
        }
        
        if (logbookEntryId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("logbookEntryId"));
        }

        await _logbookEntryRepository.Delete((int) logbookEntryId);

        return RedirectToPage("Read", new { equipmentId });
    }
}