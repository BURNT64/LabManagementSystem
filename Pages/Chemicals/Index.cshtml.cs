using LabManagementSystem.Data.Repositories.Chemicals;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Chemicals;

public class IndexModel : PageModel
{
    public readonly IChemicalRepository ChemicalRepository;

    public IndexModel(IChemicalRepository chemicalRepository)
    {
        ChemicalRepository = chemicalRepository;
    }
}