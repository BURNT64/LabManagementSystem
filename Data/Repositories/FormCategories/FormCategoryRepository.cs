using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.FormCategories;

public class FormCategoryRepository : IFormCategoryRepository
{
    private readonly LabManagementContext _context;
    
    public FormCategoryRepository(LabManagementContext context)
    {
        _context = context;
    }
    
    public async Task Insert(FormCategoryModel formCategory)
    {
        _context.FormCategories.Add(formCategory);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(FormCategoryModel formCategory)
    {
        _context.Entry(formCategory).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<FormCategoryModel>> SelectAll()
    {
        return await _context.FormCategories.ToListAsync();
    }
    
    public async Task<FormCategoryModel?> Select(string formCategoryName)
    {
        return await _context.FormCategories.FirstOrDefaultAsync(e => e.Name == formCategoryName);
    }

    public async Task Delete(string formCategoryName)
    {
        var deleteItem = await _context.FormCategories.FirstOrDefaultAsync(e => e.Name == formCategoryName);
        if (deleteItem == null) return;
        _context.Remove(deleteItem);
        await _context.SaveChangesAsync();
    }
}