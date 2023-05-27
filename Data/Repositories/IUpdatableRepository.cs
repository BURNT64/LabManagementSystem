namespace LabManagementSystem.Data.Repositories;

public interface IUpdatableRepository<in T>
{
    public Task Update(T type);
}