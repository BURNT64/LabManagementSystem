using System.ComponentModel.DataAnnotations;
using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using LabManagementSystem.API.Files;
using LabManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabManagementSystem.Data.Repositories.Equipment;
using LabManagementSystem.Data.Repositories.Rooms;

namespace LabManagementSystem.Pages.Admin.Equipment;

public class UpdateModel : PageModel
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
    
    public UpdateModel(
        IRoomRepository roomRepository,
        IEquipmentRepository equipmentRepository,
        IActiveDirectoryUserRepository activeDirectoryUserRepository
    )
    {
        RoomRepository = roomRepository;
        _equipmentRepository = equipmentRepository;
        _activeDirectoryUserRepository = activeDirectoryUserRepository;
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
        
        var user = await _activeDirectoryUserRepository.GetUser(equipment.CustodianUserObjectId);
        if (user == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveActiveDirectoryUser());
        }

        Equipment = equipment;
        CustodianEmailAddress = ActiveDirectoryUserRepository.GetUserEmail(user);

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Equipment)}.{nameof(Equipment.LocationRoom)}");
        ModelState.Remove($"{nameof(Equipment)}.{nameof(Equipment.CustodianUserObjectId)}");
        foreach (var modelError in ModelState.Values.SelectMany(modelStateValue => modelStateValue.Errors))
        {
            Console.WriteLine(modelError.ErrorMessage);
        }
        if (!ModelState.IsValid) return Page();
        
        var user = await _activeDirectoryUserRepository.GetUserByEmail(CustodianEmailAddress);
        if (user == null)
        {
            ModelState.AddModelError("ActiveDirectory", ErrorMessages.CouldNotRetrieveActiveDirectoryUser());
            return Page();
        }

        Equipment.CustodianUserObjectId = user.Id;

        if (File != null)
        {
            Equipment.DocumentationFile = await FileSerialisationHelper.ConvertFileToBase64(File);
            Equipment.DocumentationFileContentType = File.ContentType;
        }

        if (Image != null)
        {
            Equipment.Image = await FileSerialisationHelper.ConvertFileToBase64(Image);
        }
        
        await _equipmentRepository.Update(Equipment);
        
        return RedirectToPage("/Equipment/Index");
    }
    
    public async Task<IActionResult> OnPostDelete(int? equipmentId)
    {
        if (equipmentId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("equipmentId"));
        }
        
        await _equipmentRepository.Delete((int) equipmentId);
        
        return RedirectToPage("/Equipment/Index");
    }
}