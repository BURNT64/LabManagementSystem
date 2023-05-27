using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.Staff;

public class StaffRepository : IStaffRepository
{
    private readonly LabManagementContext _context;
    
    public StaffRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(StaffModel staff)
    {
        _context.Staff.Add(staff);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(StaffModel staff)
    {
        _context.Entry(staff).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<StaffModel>> SelectAll()
    {
        return await _context.Staff.ToListAsync();
    }
    
    public async Task<StaffModel?> Select(string userObjectId)
    {
        return await _context.Staff.FirstOrDefaultAsync(e => e.UserObjectId == userObjectId);
    }

    public async Task Delete(string userObjectId)
    {
        var deleteItem = await _context.Staff.FirstOrDefaultAsync(e => e.UserObjectId == userObjectId);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}