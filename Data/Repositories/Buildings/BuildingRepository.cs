using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.Buildings;

public class BuildingRepository : IBuildingRepository
{
    private readonly LabManagementContext _context;
    
    public BuildingRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(BuildingModel building)
    {
        _context.Buildings.Add(building);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(BuildingModel building)
    {
        _context.Entry(building).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<BuildingModel>> SelectAll()
    {
        return await _context.Buildings.ToListAsync();
    }
    
    public async Task<BuildingModel?> Select(string buildingName)
    {
        return await _context.Buildings.FirstOrDefaultAsync(e => e.Name == buildingName);
    }

    public async Task Delete(string buildingName)
    {
        var deleteItem = await _context.Buildings.FirstOrDefaultAsync(e => e.Name == buildingName);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}