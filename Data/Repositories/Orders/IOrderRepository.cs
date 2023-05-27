using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.Orders;

public interface IOrderRepository :
    IInsertableRepository<OrderModel>,
    IUpdatableRepository<OrderModel>,
    IEntirelySelectableRepository<OrderModel>,
    IOneKeySelectableRepository<OrderModel, int>,
    IOneKeyDeletableRepository<int>
{
    
}