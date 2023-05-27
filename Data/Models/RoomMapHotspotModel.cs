using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Data.Models;

public class RoomMapHotspotModel
{
    [Display(Name = "Room Code")]
    [Key, ForeignKey("Room")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string RoomCode { get; set; }
    public virtual RoomModel Room { get; set; }
    
    [Display(Name = "Point One X")]
    [Column(TypeName = "integer")]
    public int PointOneX { get; set; }
    
    [Display(Name = "Point One Y")]
    [Column(TypeName = "integer")]
    public int PointOneY { get; set; }
    
    [Display(Name = "Point Two X")]
    [Column(TypeName = "integer")]
    public int PointTwoX { get; set; }
    
    [Display(Name = "Point Two Y")]
    [Column(TypeName = "integer")]
    public int PointTwoY { get; set; }
}