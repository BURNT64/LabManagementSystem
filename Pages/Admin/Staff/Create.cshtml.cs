using System.ComponentModel.DataAnnotations;
using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Staff;

public class CreateModel : PageModel
{
    [BindProperty]
    public StaffModel Staff { get; set; }

    [BindProperty]
    [Display(Name = "Email Address")]
    [DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; }

    private readonly IStaffRepository _staffRepository;

    private readonly IActiveDirectoryUserRepository _activeDirectoryUserRepository;
    
    public CreateModel(IStaffRepository staffRepository, IActiveDirectoryUserRepository activeDirectoryUserRepository)
    {
        _staffRepository = staffRepository;
        _activeDirectoryUserRepository = activeDirectoryUserRepository;
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Staff)}.{nameof(Staff.UserObjectId)}");
        if (!ModelState.IsValid) return Page();
        
        var user = await _activeDirectoryUserRepository.GetUserByEmail(EmailAddress);
        if (user == null)
        {
            ModelState.AddModelError("ActiveDirectory", ErrorMessages.CouldNotRetrieveActiveDirectoryUser());
            return Page();
        }

        var existingStaff = await _staffRepository.Select(user.Id);
        if (existingStaff != null)
        {
            ModelState.AddModelError("Database", "User is already marked as a staff member.");
            return Page();
        }

        Staff.UserObjectId = user.Id;

        await _staffRepository.Insert(Staff);

        return RedirectToPage("/Admin/Staff/Index");
    }
}