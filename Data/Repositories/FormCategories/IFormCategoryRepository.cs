using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.FormCategories;

public interface IFormCategoryRepository :
    IInsertableRepository<FormCategoryModel>,
    IUpdatableRepository<FormCategoryModel>,
    IEntirelySelectableRepository<FormCategoryModel>,
    IOneKeySelectableRepository<FormCategoryModel, string>,
    IOneKeyDeletableRepository<string>
{
    
}