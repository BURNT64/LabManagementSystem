using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.LogbookEntries;

public interface ILogbookEntryRepository :
    IInsertableRepository<LogbookEntryModel>,
    IUpdatableRepository<LogbookEntryModel>,
    IEntirelySelectableRepository<LogbookEntryModel>,
    IOneKeySelectableRepository<LogbookEntryModel, int>,
    IOneKeyDeletableRepository<int>
{
    
}