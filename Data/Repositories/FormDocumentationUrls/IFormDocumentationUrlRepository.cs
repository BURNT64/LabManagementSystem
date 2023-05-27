using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.FormDocumentationUrls;

public interface IFormDocumentationUrlRepository : 
    IInsertableRepository<FormDocumentationUrlModel>,
    IUpdatableRepository<FormDocumentationUrlModel>,
    IEntirelySelectableRepository<FormDocumentationUrlModel>,
    IOneKeySelectableRepository<FormDocumentationUrlModel, int>,
    IOneKeyDeletableRepository<int>
{
    
}