using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.FormDocumentationFiles;

public interface IFormDocumentationFileRepository :
    IInsertableRepository<FormDocumentationFileModel>,
    IUpdatableRepository<FormDocumentationFileModel>,
    IEntirelySelectableRepository<FormDocumentationFileModel>,
    IOneKeySelectableRepository<FormDocumentationFileModel, int>,
    IOneKeyDeletableRepository<int>
{
    
}