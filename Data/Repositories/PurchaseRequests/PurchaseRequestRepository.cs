using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.PurchaseRequests;

public class PurchaseRequestRepository : IPurchaseRequestRepository
{
    private readonly LabManagementContext _context;

    public PurchaseRequestRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(PurchaseRequestModel purchaseRequest)
    {
        _context.PurchaseRequests.Add(purchaseRequest);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(PurchaseRequestModel purchaseRequest)
    {
        _context.Entry(purchaseRequest).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<PurchaseRequestModel>> SelectAll()
    {
        return await _context.PurchaseRequests.ToListAsync();
    }
    
    public async Task<PurchaseRequestModel?> Select(int purchaseRequestId)
    {
        return await _context.PurchaseRequests.FirstOrDefaultAsync(e => e.Id == purchaseRequestId);
    }

    public async Task Delete(int purchaseRequestId)
    {
        var deleteItem = await _context.PurchaseRequests.FirstOrDefaultAsync(e => e.Id == purchaseRequestId);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}
