using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.PurchaseRequests;

public interface IPurchaseRequestRepository :
    IInsertableRepository<PurchaseRequestModel>,
    IUpdatableRepository<PurchaseRequestModel>,
    IEntirelySelectableRepository<PurchaseRequestModel>,
    IOneKeySelectableRepository<PurchaseRequestModel, int>,
    IOneKeyDeletableRepository<int>
{
    
}