using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly LabManagementContext _context;
    
    public OrderRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(OrderModel order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(OrderModel order)
    {
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<OrderModel>> SelectAll()
    {
        return await _context.Orders.ToListAsync();
    }
    
    public async Task<OrderModel?> Select(int purchaseRequestId)
    {
        return await _context.Orders.FirstOrDefaultAsync(e => e.PurchaseRequest.Id == purchaseRequestId);
    }

    public async Task Delete(int purchaseRequestId)
    {
        var deleteItem = await _context.Orders.FirstOrDefaultAsync(e => e.PurchaseRequest.Id == purchaseRequestId);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}

