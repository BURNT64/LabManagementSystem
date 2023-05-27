using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class OrderModel
{
	[Display(Name = "Purchase Request ID")]
	[Key, ForeignKey("PurchaseRequest")]
	[Column(TypeName = "integer")]
	public int PurchaseRequestId { get; set; }
	public virtual PurchaseRequestModel PurchaseRequest { get; set; }

	[Display(Name = "Date")]
	[Column(TypeName = "datetime2")]
	[DataType(DataType.Date)]
	public DateTime Date { get; set; }

	[Display(Name = "Predicted Date")]
	[Column(TypeName = "datetime2")]
	[DataType(DataType.Date)]
	public DateTime? PredictedDate { get; set; }

	[Display(Name = "Delivered?")]
	[Column("Delivered", TypeName = "bit")]
	public bool IsDelivered { get; set; }
}
