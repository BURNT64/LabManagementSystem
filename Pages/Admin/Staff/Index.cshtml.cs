using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.Data.Repositories.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Staff;

public class IndexModel : PageModel
{
    public readonly IStaffRepository StaffRepository;

    public readonly IActiveDirectoryUserRepository ActiveDirectoryUserRepository;
    
    public IndexModel(IStaffRepository staffRepository, IActiveDirectoryUserRepository activeDirectoryUserRepository)
    {
        StaffRepository = staffRepository;
        ActiveDirectoryUserRepository = activeDirectoryUserRepository;
    }
}