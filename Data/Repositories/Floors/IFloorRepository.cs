using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.Floors;

public interface IFloorRepository :
    IInsertableRepository<FloorModel>,
    IUpdatableRepository<FloorModel>,
    IEntirelySelectableRepository<FloorModel>,
    IOneKeySelectableRepository<FloorModel, int>,
    IOneKeyDeletableRepository<int>
{
    
}