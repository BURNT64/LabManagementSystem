using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Query;

namespace LabManagementSystem.Data.Models;

public class FormCategoryModel
{
	[Display(Name = "Name")]
	[Key]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	public string Name { get; set; }
	
	public virtual ICollection<FormDocumentationUrlModel> DocumentationUrls { get; set; } =
		new List<FormDocumentationUrlModel>();

	public virtual ICollection<FormDocumentationFileModel> DocumentationFiles { get; set; } =
		new List<FormDocumentationFileModel>();
}
