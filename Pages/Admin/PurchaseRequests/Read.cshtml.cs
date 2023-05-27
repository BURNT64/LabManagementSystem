using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Orders;
using LabManagementSystem.Data.Repositories.PurchaseRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.PurchaseRequests;

public class ReadModel : PageModel
{
    public PurchaseRequestModel PurchaseRequest { get; set; }

    private readonly IPurchaseRequestRepository PurchaseRequestRepository;

    private readonly IOrderRepository OrderRepository;

    public readonly IActiveDirectoryUserRepository ActiveDirectoryUserRepository;

    public ReadModel(
        IPurchaseRequestRepository purchaseRequestRepository,
        IOrderRepository orderRepository,
        IActiveDirectoryUserRepository activeDirectoryUserRepository
    )
    {
        PurchaseRequestRepository = purchaseRequestRepository;
        OrderRepository = orderRepository;
        ActiveDirectoryUserRepository = activeDirectoryUserRepository;
    }
    
    public async Task<IActionResult> OnGet(int? requestId)
    {
        if (requestId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("requestId"));
        }
        
        var purchaseRequest = await PurchaseRequestRepository.Select((int) requestId);
        if (purchaseRequest == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("purchase request"));
        }

        PurchaseRequest = purchaseRequest;
        
        return Page();
    }

    public async Task<IActionResult> OnPostApprove(int? requestId)
    {
        if (requestId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("requestId"));
        }
        
        var purchaseRequest = await PurchaseRequestRepository.Select((int) requestId);
        if (purchaseRequest == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("purchase request"));
        }

        purchaseRequest.IsProcessed = true;

        await PurchaseRequestRepository.Update(purchaseRequest);

        OrderModel newOrder = new OrderModel
        {
            PurchaseRequestId = purchaseRequest.Id,
            Date = DateTime.Now
        };

        await OrderRepository.Insert(newOrder);
        
        return RedirectToPage("Index");
    }
    
    public async Task<IActionResult> OnPostDecline(int? requestId)
    {
        if (requestId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("requestId"));
        }
        
        var purchaseRequest = await PurchaseRequestRepository.Select((int) requestId);
        if (purchaseRequest == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("purchase request"));
        }

        purchaseRequest.IsProcessed = true;

        await PurchaseRequestRepository.Update(purchaseRequest);

        return RedirectToPage("Index");
    }
}