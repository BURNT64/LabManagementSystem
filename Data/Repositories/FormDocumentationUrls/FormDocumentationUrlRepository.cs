using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.FormDocumentationUrls;

public class FormDocumentationUrlRepository : IFormDocumentationUrlRepository
{
	private readonly LabManagementContext _context;
	
	public FormDocumentationUrlRepository(LabManagementContext context) 
	{ 
		_context = context; 
	}
	
	public async Task Insert(FormDocumentationUrlModel formDocumentationUrl)
	{
		_context.FormDocumentationUrls.Add(formDocumentationUrl);
		await _context.SaveChangesAsync();
	}
	
	public async Task Update(FormDocumentationUrlModel formDocumentationUrl)
	{
		_context.Entry(formDocumentationUrl).State = EntityState.Modified;
		await _context.SaveChangesAsync();
	}
	
	public async Task<ICollection<FormDocumentationUrlModel>> SelectAll()
	{
		return await _context.FormDocumentationUrls.ToListAsync();
	}
	
	public async Task<FormDocumentationUrlModel?> Select(int urlDocumentationId)
	{
		return await _context.FormDocumentationUrls.FirstOrDefaultAsync(e => e.Id == urlDocumentationId);
	}

	public async Task Delete(int urlDocumentationId)
	{
		var deleteItem = await _context.FormDocumentationUrls.FirstOrDefaultAsync(e => e.Id == urlDocumentationId);
		if (deleteItem == null) return;
		_context.Remove(deleteItem);
		await _context.SaveChangesAsync();
	}
}
