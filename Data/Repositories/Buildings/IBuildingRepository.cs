using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.Buildings;

public interface IBuildingRepository :
    IInsertableRepository<BuildingModel>,
    IUpdatableRepository<BuildingModel>,
    IEntirelySelectableRepository<BuildingModel>,
    IOneKeySelectableRepository<BuildingModel, string>,
    IOneKeyDeletableRepository<string>
{
    
}