using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class BuildingModel
{
    [Display(Name = "Name")]
    [Key]
    [Column("BuildingName", TypeName = "nvarchar")]
    [StringLength(255)]
    public string Name { get; set; }

    [Display(Name = "Image")]
    [Column(TypeName = "varbinary(max)")]
    public byte[] Image { get; set; }
    
    [Display(Name = "Campus Name")]
    [Column("CampusName", TypeName = "nvarchar")]
    [StringLength(255)]
    public string CampusName { get; set; }
    [ForeignKey("CampusName")]
    public virtual CampusModel Campus { get; set; }

    public virtual ICollection<FloorModel> Floors { get; set; } =
        new List<FloorModel>();

    public virtual ICollection<PurchaseRequestModel> PurchaseRequests { get; set; } =
        new List<PurchaseRequestModel>();
}