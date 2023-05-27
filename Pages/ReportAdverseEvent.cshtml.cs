using Microsoft.AspNetCore.Mvc.RazorPages;
using LabManagementSystem.Data.Repositories.Campuses;
using LabManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabManagementSystem.Pages;

public class ReportAdverseEventModel : PageModel
{
    public readonly ICampusRepository CampusRepository;

    public ReportAdverseEventModel(ICampusRepository campusRepository)
    {
        CampusRepository = campusRepository;
    }
}