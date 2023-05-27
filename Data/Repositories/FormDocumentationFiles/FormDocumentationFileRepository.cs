using LabManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Repositories.FormDocumentationFiles;

public class FormDocumentationFileRepository : IFormDocumentationFileRepository
{
	private readonly LabManagementContext _context;
	
	public FormDocumentationFileRepository(LabManagementContext context)
	{
		_context = context; 
	}
	
	public async Task Insert(FormDocumentationFileModel formDocumentationFile)
	{
		_context.FormDocumentationFiles.Add(formDocumentationFile);
		await _context.SaveChangesAsync();
	}
	
	public async Task Update(FormDocumentationFileModel formDocumentationFile)
	{
		_context.Entry(formDocumentationFile).State = EntityState.Modified;
		await _context.SaveChangesAsync();
	}

	public async Task<ICollection<FormDocumentationFileModel>> SelectAll()
	{
		return await _context.FormDocumentationFiles.ToListAsync();
	}
	
	public async Task<FormDocumentationFileModel?> Select(int fileDocumentationId)
	{
		return await _context.FormDocumentationFiles.FirstOrDefaultAsync(e => e.Id == fileDocumentationId);
	}

	public async Task Delete(int fileDocumentationId)
	{
		var deleteItem = await _context.FormDocumentationFiles.FirstOrDefaultAsync(e => e.Id == fileDocumentationId);
		if (deleteItem == null) return;
		_context.Remove(deleteItem);
		await _context.SaveChangesAsync();
	}
}