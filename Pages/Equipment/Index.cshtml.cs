using Microsoft.AspNetCore.Mvc.RazorPages;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Equipment;
using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LabManagementSystem.Pages.Equipment;

public class IndexModel : PageModel
{
    public readonly IEquipmentRepository EquipmentRepository;
    
    public IndexModel(IEquipmentRepository equipmentRepository)
    {
        EquipmentRepository = equipmentRepository;
    }
    
    public async Task<IActionResult> OnGetDownloadFile(int? equipmentId)
    {
        if (equipmentId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("equipmentId"));
        }

        var equipment = await EquipmentRepository.Select((int) equipmentId);
        if (equipment == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("equipment"));
        }

        if (equipment.DocumentationFile == null)
        {
            return NotFound(ErrorMessages.NoFileToDownload());
        }
        
        return File(equipment.DocumentationFile, equipment.DocumentationFileContentType!, equipment.Name);
    }
}