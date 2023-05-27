using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class RoomDocumentationFileModel
{
    [Display(Name = "ID")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("FileDocumentationId", TypeName = "integer")]
    public int Id { get; set; }
    
    [Display(Name = "Room Code")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string RoomCode { get; set; }
    [ForeignKey("RoomCode")]
    public virtual RoomModel Room { get; set; }
    
    [Display(Name = "File")]
    [Column(TypeName = "varbinary(max)")]
    public byte[] File { get; set; }
    
    [Display(Name = "Name")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Name { get; set; }
	
    [Display(Name = "ContentType")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string ContentType { get; set; }
}