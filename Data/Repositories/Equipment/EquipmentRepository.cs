using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.Equipment;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly LabManagementContext _context;
    
    public EquipmentRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(EquipmentModel equipment)
    {
        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(EquipmentModel equipment)
    {
        _context.Entry(equipment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<EquipmentModel>> SelectAll()
    {
        return await _context.Equipment.ToListAsync();
    }
    
    public async Task<EquipmentModel?> Select(int equipmentId)
    { 
        return await _context.Equipment.FirstOrDefaultAsync(e => e.Id == equipmentId);
    }

    public async Task Delete(int equipmentId)
    {
        var deleteItem = await _context.Equipment.FirstOrDefaultAsync(e => e.Id == equipmentId);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}