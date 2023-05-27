using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class FloorModel
{
    [Display(Name = "ID")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("FloorId", TypeName = "integer")]
    public int Id { get; set; }

    [Display(Name = "Name")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Name { get; set; }
    
    [Display(Name = "Building Name")]
    [Column("BuildingName", TypeName = "nvarchar")]
    [StringLength(255)]
    public string BuildingName { get; set; }
    [ForeignKey("BuildingName")]
    public virtual BuildingModel Building { get; set; }
    
    [Display(Name = "Map Image")]
    [Column(TypeName = "varbinary(max)")]
    public byte[] MapImage { get; set; }

    public virtual ICollection<RoomModel> Rooms { get; set; } = new List<RoomModel>();
}