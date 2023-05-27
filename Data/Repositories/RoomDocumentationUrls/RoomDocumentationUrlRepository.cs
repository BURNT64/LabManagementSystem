using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.RoomDocumentationUrls;

public class RoomDocumentationUrlRepository : IRoomDocumentationUrlRepository
{
    private readonly LabManagementContext _context;
    
    public RoomDocumentationUrlRepository(LabManagementContext context)
    {
        _context = context;
    }
    

    public async Task Insert(RoomDocumentationUrlModel roomDocumentationUrl)
    {
        _context.RoomDocumentationUrls.Add(roomDocumentationUrl);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(RoomDocumentationUrlModel roomDocumentationUrl)
    {
        _context.Entry(roomDocumentationUrl).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<RoomDocumentationUrlModel>> SelectAll()
    {
        return await _context.RoomDocumentationUrls.ToListAsync();
    }
    
    public async Task<RoomDocumentationUrlModel?> Select(int urlDocumentationId)
    {
        return await _context.RoomDocumentationUrls.FirstOrDefaultAsync(e => e.Id == urlDocumentationId);
    }

    public async Task Delete(int urlDocumentationId)
    {
        var deleteItem = await _context.RoomDocumentationUrls.FirstOrDefaultAsync(e => e.Id == urlDocumentationId);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}