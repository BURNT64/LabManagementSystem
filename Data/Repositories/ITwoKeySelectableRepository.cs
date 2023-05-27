namespace LabManagementSystem.Data.Repositories;

public interface ITwoKeySelectableRepository<TType, in TKeyOne, in TKeyTwo>
{
    public Task<TType?> Select(TKeyOne keyOne, TKeyTwo keyTwo);
}