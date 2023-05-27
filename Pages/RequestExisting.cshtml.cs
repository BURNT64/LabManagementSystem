using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.Data.Repositories.Chemicals;
using LabManagementSystem.Data.Repositories.Equipment;
using LabManagementSystem.Data.Repositories.Rooms;
using LabManagementSystem.Data.Repositories.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages;

[Authorize]
public class RequestExistingModel : PageModel
{
    public readonly IChemicalRepository ChemicalRepository;
   
    public readonly IEquipmentRepository EquipmentRepository;
    
    public readonly IRoomRepository RoomRepository;
    
    public readonly IStaffRepository StaffRepository;
    
    public readonly IActiveDirectoryUserRepository ActiveDirectoryUserRepository;
    
    public RequestExistingModel(
        IChemicalRepository chemicalRepository,
        IEquipmentRepository equipmentRepository,
        IRoomRepository roomRepository,
        IStaffRepository staffRepository,
        IActiveDirectoryUserRepository activeDirectoryUserRepository
    )
    {
        ChemicalRepository = chemicalRepository;
        EquipmentRepository = equipmentRepository;
        RoomRepository = roomRepository;
        StaffRepository = staffRepository;
        ActiveDirectoryUserRepository = activeDirectoryUserRepository;
    }
}
