using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.Floors;

public class FloorRepository : IFloorRepository
{
    private readonly LabManagementContext _context;
    
    public FloorRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(FloorModel floor)
    {
        _context.Floors.Add(floor);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(FloorModel floor)
    {
        _context.Entry(floor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<FloorModel>> SelectAll()
    {
        return await _context.Floors.ToListAsync();
    }
    
    public async Task<FloorModel?> Select(int floorId)
    {
        return await _context.Floors.FirstOrDefaultAsync(e => e.Id == floorId);
    }

    public async Task Delete(int floorId)
    {
        var deleteItem = await _context.Floors.FirstOrDefaultAsync(e => e.Id == floorId);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}