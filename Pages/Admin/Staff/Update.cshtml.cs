using System.ComponentModel.DataAnnotations;
using System.Drawing;
using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Staff;

public class UpdateModel : PageModel
{
    [BindProperty]
    public StaffModel Staff { get; set; }

    [BindProperty]
    [Display(Name = "Email Address")]
    [DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; }

    public readonly IStaffRepository StaffRepository;

    private readonly IActiveDirectoryUserRepository _activeDirectoryUserRepository;

    public UpdateModel(IStaffRepository staffRepository, IActiveDirectoryUserRepository activeDirectoryUserRepository)
    {
        StaffRepository = staffRepository;
        _activeDirectoryUserRepository = activeDirectoryUserRepository;
    }
    
    public async Task<IActionResult> OnGet(string? userObjectId)
    {
        if (userObjectId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("userObjectId"));
        }
        
        var user = await _activeDirectoryUserRepository.GetUser(userObjectId);
        if (user == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveActiveDirectoryUser());
        }

        var staff = await StaffRepository.Select(userObjectId);
        if (staff == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("staff"));
        }

        Staff = staff;
        EmailAddress = ActiveDirectoryUserRepository.GetUserEmail(user);

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();
        
        var user = await _activeDirectoryUserRepository.GetUserByEmail(EmailAddress);
        if (user == null)
        {
            ModelState.AddModelError("ActiveDirectory", ErrorMessages.CouldNotRetrieveActiveDirectoryUser());
            return Page();
        }

        await StaffRepository.Update(Staff);
        
        return RedirectToPage("/Admin/Staff/Index");
    }
    
    public async Task<IActionResult> OnPostDelete(string? emailAddress)
    {
        if (emailAddress == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("emailAddress"));
        }
        
        var user = await _activeDirectoryUserRepository.GetUserByEmail(emailAddress);
        if (user == null)
        {
            ModelState.AddModelError("ActiveDirectory", ErrorMessages.CouldNotRetrieveActiveDirectoryUser());
            return Page();
        }
        
        await StaffRepository.Delete(user.Id);
        
        return RedirectToPage("/Admin/Staff/Index");
    }
}