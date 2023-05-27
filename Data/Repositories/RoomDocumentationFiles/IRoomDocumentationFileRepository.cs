using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.RoomDocumentationFiles;

public interface IRoomDocumentationFileRepository :
    IInsertableRepository<RoomDocumentationFileModel>,
    IUpdatableRepository<RoomDocumentationFileModel>,
    IEntirelySelectableRepository<RoomDocumentationFileModel>,
    IOneKeySelectableRepository<RoomDocumentationFileModel, int>,
    IOneKeyDeletableRepository<int>
{
    
}