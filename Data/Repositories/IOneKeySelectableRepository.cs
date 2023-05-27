namespace LabManagementSystem.Data.Repositories;

public interface IOneKeySelectableRepository<TType, in TKeyOne>
{
    public Task<TType?> Select(TKeyOne keyOne);
}