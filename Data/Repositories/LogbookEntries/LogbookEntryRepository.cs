using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.LogbookEntries;

public class LogbookEntryRepository : ILogbookEntryRepository
{
    private readonly LabManagementContext _context;
    
    public LogbookEntryRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(LogbookEntryModel logbookEntry)
    {
        _context.LogbookEntries.Add(logbookEntry);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(LogbookEntryModel logbookEntry)
    {
        _context.Entry(logbookEntry).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<LogbookEntryModel>> SelectAll()
    {
        return await _context.LogbookEntries.ToListAsync();
    }
    
    public async Task<LogbookEntryModel?> Select(int entryId)
    {
        return await _context.LogbookEntries.FirstOrDefaultAsync(e => e.Id == entryId);
    }

    public async Task Delete(int entryId)
    {
        var deleteItem = await _context.LogbookEntries.FirstOrDefaultAsync(e => e.Id == entryId);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}

