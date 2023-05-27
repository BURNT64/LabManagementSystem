using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class RoomModel
{
    [Display(Name = "Code")]
    [Key]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Code { get; set; }
    
    [Display(Name = "Name")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Name { get; set; }

    [Display(Name = "Timetable URL")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    [DataType(DataType.Url)]
    public string? TimetableUrl { get; set; }
    
    [Display(Name = "Floor ID")]
    [Column(TypeName = "integer")]
    public int FloorId { get; set; }
    [ForeignKey("FloorId")]
    public virtual FloorModel Floor { get; set; }

    public virtual RoomMapHotspotModel MapHotspot { get; set; }
    
    public virtual ICollection<RoomDocumentationFileModel> DocumentationFiles { get; set; } =
        new List<RoomDocumentationFileModel>();

    public virtual ICollection<RoomDocumentationUrlModel> DocumentationUrls { get; set; } =
        new List<RoomDocumentationUrlModel>();

    public virtual ICollection<EquipmentModel> Equipment { get; set; } =
        new List<EquipmentModel>();

    public virtual ICollection<ChemicalModel> Chemicals { get; set; } =
        new List<ChemicalModel>();
}