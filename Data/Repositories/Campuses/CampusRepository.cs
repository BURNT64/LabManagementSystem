using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.Campuses;

public class CampusRepository : ICampusRepository
{
    private readonly LabManagementContext _context;
    
    public CampusRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(CampusModel campus)
    {
        _context.Campuses.Add(campus);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(CampusModel campus)
    {
        _context.Entry(campus).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<CampusModel>> SelectAll()
    {
        return await _context.Campuses.ToListAsync();
    }
    
    public async Task<CampusModel?> Select(string campusName)
    {
        return await _context.Campuses.FirstOrDefaultAsync(e => e.Name == campusName);
    }

    public async Task Delete(string campusName)
    {
        var deleteItem = await _context.Campuses.FirstOrDefaultAsync(e => e.Name == campusName);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}

