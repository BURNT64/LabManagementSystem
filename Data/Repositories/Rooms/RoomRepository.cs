using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.Rooms;

public class RoomRepository : IRoomRepository
{
    private readonly LabManagementContext _context;
    
    public RoomRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(RoomModel room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(RoomModel room)
    {
        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<RoomModel>> SelectAll()
    {
        return await _context.Rooms.ToListAsync();
    }
    
    public async Task<RoomModel?> Select(string roomCode)
    {
        return await _context.Rooms.FirstOrDefaultAsync(e => e.Code == roomCode);
    }

    public async Task Delete(string roomCode)
    {
        var deleteItem = await _context.Rooms.FirstOrDefaultAsync(e => e.Code == roomCode);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}