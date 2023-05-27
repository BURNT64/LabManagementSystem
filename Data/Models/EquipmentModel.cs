using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class EquipmentModel
{
    [Display(Name = "ID")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("EquipmentId", TypeName = "integer")]
    public int Id { get; set; }
    
    [Display(Name = "Name")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Name { get; set; }
    
    [Display(Name = "Custodian User Object ID")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string CustodianUserObjectId { get; set; }
    
    [Display(Name = "General Information")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string GeneralInfo { get; set; }
    
    [Display(Name = "Location Room Code")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string LocationRoomCode { get; set; }
    [ForeignKey("LocationRoomCode")]
    public virtual RoomModel LocationRoom { get; set; }
    
    [Display(Name = "Documentation URL")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    [DataType(DataType.Url)]
    public string? DocumentationUrl { get; set; }

    [Display(Name = "Documentation File")]
    [Column(TypeName = "varbinary(max)")]
    public byte[]? DocumentationFile { get; set; }

    [Display(Name = "Documentation File ContentType")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string? DocumentationFileContentType { get; set; }
    
    [Display(Name = "Image")]
    [Column(TypeName = "varbinary(max)")]
    public byte[]? Image { get; set; }

    public virtual ICollection<LogbookEntryModel> LogbookEntries { get; set; } =
        new List<LogbookEntryModel>();
}