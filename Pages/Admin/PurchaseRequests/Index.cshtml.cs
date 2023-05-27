using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.PurchaseRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.PurchaseRequests;

public class IndexModel : PageModel
{
    public ICollection<PurchaseRequestModel> PendingPurchaseRequests = new List<PurchaseRequestModel>();
    
    private readonly IPurchaseRequestRepository PurchaseRequestRepository;

    public readonly IActiveDirectoryUserRepository ActiveDirectoryUserRepository;
    
    public IndexModel(
        IPurchaseRequestRepository purchaseRequestRepository,
        IActiveDirectoryUserRepository activeDirectoryUserRepository
    )
    {
        PurchaseRequestRepository = purchaseRequestRepository;
        ActiveDirectoryUserRepository = activeDirectoryUserRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        foreach (var purchaseRequest in await PurchaseRequestRepository.SelectAll())
        {
            if (purchaseRequest.IsProcessed) continue;
            PendingPurchaseRequests.Add(purchaseRequest);
        }

        return Page();
    }
}