using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class RoomDocumentationUrlModel
{
    [Display(Name = "ID")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("UrlDocumentationId", TypeName = "integer")]
    public int Id { get; set; }
    
    [Display(Name = "Room Code")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string RoomCode { get; set; }
    [ForeignKey("RoomCode")]
    public virtual RoomModel Room { get; set; }
    
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