using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories;
using LabManagementSystem.Data.Repositories.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;

namespace LabManagementSystem.Pages;

public class ContactsModel : PageModel
{
    public readonly IStaffRepository StaffRepository;

    public readonly IActiveDirectoryUserRepository ActiveDirectoryUserRepository;

    public ContactsModel(IStaffRepository staffRepository, IActiveDirectoryUserRepository activeDirectoryUserRepository)
    {
        StaffRepository = staffRepository;
        ActiveDirectoryUserRepository = activeDirectoryUserRepository;
    }
}