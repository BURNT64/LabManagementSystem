using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class FormDocumentationUrlModel
{
	[Display(Name = "ID")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("UrlDocumentationId", TypeName = "integer")]
	public int Id { get; set; }
	
	[Display(Name = "Category Name")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	public string CategoryName { get; set; }
	[ForeignKey("CategoryName")]
	public virtual FormCategoryModel Category { get; set; }

	[Display(Name = "URL")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	[DataType(DataType.Url)]
	public string Url { get; set; }
	
	[Display(Name = "Name")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	public string Name { get; set; }
}
