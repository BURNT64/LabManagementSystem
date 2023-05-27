using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.RoomDocumentationFiles;

public class RoomDocumentationFileRepository : IRoomDocumentationFileRepository
{
    private readonly LabManagementContext _context;
    
    public RoomDocumentationFileRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(RoomDocumentationFileModel roomDocumentationFile)
    {
        _context.RoomDocumentationFiles.Add(roomDocumentationFile);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(RoomDocumentationFileModel roomDocumentationFile)
    {
        _context.Entry(roomDocumentationFile).State = EntityState.Modified;          
        await _context.SaveChangesAsync();
    }
    
    public async Task<ICollection<RoomDocumentationFileModel>> SelectAll()
    {
        return await _context.RoomDocumentationFiles.ToListAsync();
    }
    
    public async Task<RoomDocumentationFileModel?> Select(int fileDocumentationId)
    {
        return await _context.RoomDocumentationFiles.FirstOrDefaultAsync(e => e.Id == fileDocumentationId);
    }

    public async Task Delete(int fileDocumentationId)
    {
        var deleteItem = await _context.RoomDocumentationFiles.FirstOrDefaultAsync(e => e.Id == fileDocumentationId);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}