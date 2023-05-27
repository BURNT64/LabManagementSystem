using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.Staff;

public interface IStaffRepository :
    IInsertableRepository<StaffModel>,
    IUpdatableRepository<StaffModel>,
    IEntirelySelectableRepository<StaffModel>,
    IOneKeySelectableRepository<StaffModel, string>,
    IOneKeyDeletableRepository<string>
{
    
}