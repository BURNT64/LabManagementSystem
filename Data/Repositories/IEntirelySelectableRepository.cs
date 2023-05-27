namespace LabManagementSystem.Data.Repositories;

public interface IEntirelySelectableRepository<T>
{
    public Task<ICollection<T>> SelectAll();
}