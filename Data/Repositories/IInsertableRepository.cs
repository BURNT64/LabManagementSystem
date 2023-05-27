namespace LabManagementSystem.Data.Repositories;

public interface IInsertableRepository<in T>
{
    public Task Insert(T type);
}