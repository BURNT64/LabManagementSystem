using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.Rooms;

public interface IRoomRepository :
    IInsertableRepository<RoomModel>,
    IUpdatableRepository<RoomModel>,
    IEntirelySelectableRepository<RoomModel>,
    IOneKeySelectableRepository<RoomModel, string>,
    IOneKeyDeletableRepository<string>
{
    
}