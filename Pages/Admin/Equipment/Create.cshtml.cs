using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using LabManagementSystem.API.Files;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Equipment;
using LabManagementSystem.Data.Repositories.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Equipment;

public class CreateModel : PageModel
{
    [BindProperty]
    public EquipmentModel Equipment { get; set; }
    
    [BindProperty]
    [Display(Name = "Custodian Email Address")]
    [DataType(DataType.EmailAddress)]
    public string CustodianEmailAddress { get; set; }
    
    [BindProperty]
    [Display(Name = "Documentation File")]
    public IFormFile? File { get; set; }
    
    [BindProperty]
    [Display(Name = "Image")]
    public IFormFile? Image { get; set; }
    
    public readonly IRoomRepository RoomRepository;

    private readonly IEquipmentRepository _equipmentRepository;
    
    private readonly IActiveDirectoryUserRepository _activeDirectoryUserRepository;
    
    public CreateModel(
        IRoomRepository roomRepository,
        IEquipmentRepository equipmentRepository,
        IActiveDirectoryUserRepository activeDirectoryUserRepository
    )
    {
        RoomRepository = roomRepository;
        _equipmentRepository = equipmentRepository;
        _activeDirectoryUserRepository = activeDirectoryUserRepository;
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Equipment)}.{nameof(Equipment.LocationRoom)}");
        ModelState.Remove($"{nameof(Equipment)}.{nameof(Equipment.CustodianUserObjectId)}");
        if (!ModelState.IsValid) return Page();
        
        var user = await _activeDirectoryUserRepository.GetUserByEmail(CustodianEmailAddress);
        if (user == null)
        {
            ModelState.AddModelError("ActiveDirectory", ErrorMessages.CouldNotRetrieveActiveDirectoryUser());
            return Page();
        }
        
        if (File != null)
        {
            Equipment.DocumentationFile = await FileSerialisationHelper.ConvertFileToBase64(File);
            Equipment.DocumentationFileContentType = File.ContentType;
        }

        if (Image != null)
        {
            Equipment.Image = await FileSerialisationHelper.ConvertFileToBase64(Image);
        }

        Equipment.CustodianUserObjectId = user.Id;

        await _equipmentRepository.Insert(Equipment);
        
        return RedirectToPage("/Equipment/Index");
    }
}
