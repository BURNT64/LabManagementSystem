using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagementSystem.Data.Models;

public class CampusModel
{
    [Display(Name = "Name")]
    [Key]
    [Column("CampusName", TypeName = "nvarchar")]
    [StringLength(255)]
    public string Name { get; set; }

    public virtual ICollection<BuildingModel> Buildings { get; set; } =
        new List<BuildingModel>();
}