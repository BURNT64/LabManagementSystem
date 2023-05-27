namespace LabManagementSystem.Data.Repositories;

public interface IOneKeyDeletableRepository<in TKeyOne>
{
    public Task Delete(TKeyOne keyOne);
}