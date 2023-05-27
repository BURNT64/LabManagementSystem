using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.Campuses;

public interface ICampusRepository :
    IInsertableRepository<CampusModel>,
    IUpdatableRepository<CampusModel>,
    IEntirelySelectableRepository<CampusModel>,
    IOneKeySelectableRepository<CampusModel, string>,
    IOneKeyDeletableRepository<string>
{
    
}