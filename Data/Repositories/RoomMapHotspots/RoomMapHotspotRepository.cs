using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.RoomMapHotspots;

public class RoomMapHotspotRepository : IRoomMapHotspotRepository
{
    private readonly LabManagementContext _context;
    
    public RoomMapHotspotRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(RoomMapHotspotModel roomMapHotspot)
    {
        _context.RoomMapHotspots.Add(roomMapHotspot);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(RoomMapHotspotModel roomMapHotspot)
    {
        _context.Entry(roomMapHotspot).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<RoomMapHotspotModel>> SelectAll()
    {
        return await _context.RoomMapHotspots.ToListAsync();
    }
    
    public async Task<RoomMapHotspotModel?> Select(string roomCode)
    {
        return await _context.RoomMapHotspots.FirstOrDefaultAsync(e => e.RoomCode == roomCode);
    }

    public async Task Delete(string roomCode)
    {
        var deleteItem = await _context.RoomMapHotspots.FirstOrDefaultAsync(e => e.RoomCode == roomCode);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}