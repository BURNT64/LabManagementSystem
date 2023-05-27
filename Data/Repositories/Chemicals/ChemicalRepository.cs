using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.Chemicals;

public class ChemicalRepository : IChemicalRepository
{
    private readonly LabManagementContext _context;
    
    public ChemicalRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(ChemicalModel chemical)
    {
        _context.Chemicals.Add(chemical);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(ChemicalModel chemical)
    {
        _context.Entry(chemical).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<ChemicalModel>> SelectAll()
    {
        return await _context.Chemicals.ToListAsync();
    }
    
    public async Task<ChemicalModel?> Select(int chemicalId)
    {
        return await _context.Chemicals.FirstOrDefaultAsync(e => e.Id == chemicalId);
    }

    public async Task Delete(int chemicalId)
    {
        var deleteItem = await _context.Chemicals.FirstOrDefaultAsync(e => e.Id == chemicalId);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}