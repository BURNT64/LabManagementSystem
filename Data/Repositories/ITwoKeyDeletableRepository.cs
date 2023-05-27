namespace LabManagementSystem.Data.Repositories;

public interface ITwoKeyDeletableRepository<in TKeyOne, in TKeyTwo>
{
    public Task Delete(TKeyOne keyOne, TKeyTwo keyTwo);
}