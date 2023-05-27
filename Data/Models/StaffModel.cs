using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class StaffModel
{
    [Display(Name = "User Object ID")]
    [Key]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string UserObjectId { get; set; }
    
    [Display(Name = "Role")]
    [Column(TypeName = "nvarchar")]
    [StringLength(255)]
    public string Role { get; set; }
}