using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class LogbookEntryModel
{
    [Display(Name = "ID")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("EntryId", TypeName = "integer")]
    public int Id { get; set; }
    
    [Display(Name = "Date")]
    [Column(TypeName = "datetime2")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    
    [Display(Name = "User Object ID")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string UserObjectId { get; set; }
    
    [Display(Name = "Comment")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Comment { get; set; }
    
    [Display(Name = "Equipment ID")]
    [Column(TypeName = "integer")]
    public int EquipmentId { get; set; }
    [ForeignKey("EquipmentId")]
    public virtual EquipmentModel Equipment { get; set; }
}