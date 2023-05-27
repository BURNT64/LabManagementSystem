using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Models;

public class ChemicalModel
{
    [Display(Name = "ID")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ChemicalId", TypeName = "integer")]
    public int Id { get; set; }

    [Display(Name = "Name")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Name { get; set; }

    [Display(Name = "Formula")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Formula { get; set; }

    [Display(Name = "Location Room Code")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string LocationRoomCode { get; set; }
    [ForeignKey("LocationRoomCode")]
    public virtual RoomModel LocationRoom { get; set; }

    [Display(Name = "Location Description")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string LocationDescription { get; set; }

    [Display(Name = "Unit Type")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string UnitType { get; set; }

    [Display(Name = "Trigger Level")]
    [Column(TypeName = "decimal")]
    [Precision(20, 10)]
    public double TriggerLevel { get; set; }

    [Display(Name = "Stock Level")]
    [Column(TypeName = "decimal")]
    [Precision(20, 10)]
    public double StockLevel { get; set; }

    [Display(Name = "Supplier")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Supplier { get; set; }
    
    [Display(Name = "Review Date")]
    [Column(TypeName = "datetime2")]
    [DataType(DataType.Date)]
    public DateTime ReviewDate { get; set; }
    
    [Display(Name = "Ordered By Object ID")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string OrderedByObjectId { get; set; }
    
    [Display(Name = "Hazards")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string? Hazards { get; set; }
    
    [Display(Name = "Expire Date")]
    [Column(TypeName = "datetime2")]
    [DataType(DataType.Date)]
    public DateTime ExpireDate { get; set; }
    
    [Display(Name = "Purity/Grade")]
    [Column(TypeName = "decimal")]
    [Precision(20, 10)]
    public double PurityGrade { get; set; }
    
    [Display(Name = "Batch Code")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string BatchCode { get; set; }
    
    [Display(Name = "CAS Code")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string CasCode { get; set; }
    
    [Display(Name = "Product Code")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string ProductCode { get; set; }
    
    [Display(Name = "Purpose")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Purpose { get; set; }
    
    [Display(Name = "MSDS URL")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    [DataType(DataType.Url)]
    public string? MsdsUrl { get; set; }
    
    [Display(Name = "COSHH Assessment Completed?")]
    [Column("COSHHCompleted", TypeName = "bit")]
    public bool IsCoshhCompleted { get; set; }
}
    
