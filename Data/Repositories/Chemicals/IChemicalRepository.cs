using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.Chemicals;

public interface IChemicalRepository :
    IInsertableRepository<ChemicalModel>,
    IUpdatableRepository<ChemicalModel>,
    IEntirelySelectableRepository<ChemicalModel>,
    IOneKeySelectableRepository<ChemicalModel, int>,
    IOneKeyDeletableRepository<int>
{
    
}