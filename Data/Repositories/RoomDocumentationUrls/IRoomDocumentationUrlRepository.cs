using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.RoomDocumentationUrls;

public interface IRoomDocumentationUrlRepository :
    IInsertableRepository<RoomDocumentationUrlModel>,
    IUpdatableRepository<RoomDocumentationUrlModel>,
    IEntirelySelectableRepository<RoomDocumentationUrlModel>,
    IOneKeySelectableRepository<RoomDocumentationUrlModel, int>,
    IOneKeyDeletableRepository<int>
{
    
}