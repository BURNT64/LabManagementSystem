using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Models;

public class PurchaseRequestModel
{
	[Display(Name = "ID")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("RequestId", TypeName = "integer")]
	public int Id { get; set; }

	[Display(Name = "Requester User Object ID")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	public string RequesterObjectId { get; set; }

	[Display(Name = "Item Code")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	public string ItemCode { get; set; }

	[Display(Name = "Description")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	public string Description { get; set; }
 
	[Display(Name = "Supplier")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	public string Supplier { get; set; }
 
	[Display(Name = "Unit Price (without VAT)")]
	[Column(TypeName = "decimal")]
	[Precision(5, 2)]
	public double UnitPrice { get; set; }
 
	[Display(Name = "Quantity")]
	[Column(TypeName = "integer")]
	public int Quantity { get; set; }
 
	[Display(Name = "Budget Code")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	public string BudgetCode { get; set; }
 
	[Display(Name = "Date")]
	[Column(TypeName = "datetime2")]
	[DataType(DataType.Date)]
	public DateTime Date { get; set; }
 
	[Display(Name = "URL")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	[DataType(DataType.Url)]
	public string? Url { get; set; }
 
	[Display(Name = "COSHH Assessment Required?")]
	[Column("COSHHRequired", TypeName = "bit")]
	public bool IsCOSHHRequired { get; set; }
	
	[Display(Name = "Delivery Building Name")]
	[Column(TypeName = "nvarchar")]
	[StringLength(255)]
	public string DeliveryBuildingName { get; set; }
	[ForeignKey("DeliveryBuildingName")]
	public virtual BuildingModel DeliveryBuilding { get; set; }

	[Display(Name = "Processed?")]
	[Column("Processed", TypeName = "bit")]
	public bool IsProcessed { get; set; }

	public virtual ICollection<OrderModel> Orders { get; set; } =
		new List<OrderModel>();
}
