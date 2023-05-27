using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.RoomMapHotspots;

public interface IRoomMapHotspotRepository :
    IInsertableRepository<RoomMapHotspotModel>,
    IUpdatableRepository<RoomMapHotspotModel>,
    IEntirelySelectableRepository<RoomMapHotspotModel>,
    IOneKeySelectableRepository<RoomMapHotspotModel, string>,
    IOneKeyDeletableRepository<string>
{
    
}