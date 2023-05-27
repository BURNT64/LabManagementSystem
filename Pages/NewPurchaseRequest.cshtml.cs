using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Buildings;
using LabManagementSystem.Data.Repositories.PurchaseRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;

namespace LabManagementSystem.Pages;

[Authorize]
public class NewPurchaseRequestModel : PageModel
{
	[BindProperty]
	public PurchaseRequestModel PurchaseRequest { get; set; }
	
	[BindProperty]
	[DisplayName("Requester Email Address")]
	[DataType(DataType.EmailAddress)]
	public string RequesterEmailAddress { get; set; }
	
    public readonly IPurchaseRequestRepository PurchaseRequestRepository;
    
    public readonly IBuildingRepository BuildingRepository;

    private readonly IActiveDirectoryUserRepository _activeDirectoryUserRepository;

    public NewPurchaseRequestModel(
	    IPurchaseRequestRepository purchaseRequestRepository,
	    IBuildingRepository buildingRepository,
	    IActiveDirectoryUserRepository activeDirectoryUserRepository
	)
    {
        PurchaseRequestRepository = purchaseRequestRepository;
        BuildingRepository = buildingRepository;
        _activeDirectoryUserRepository = activeDirectoryUserRepository;
    }

    public async Task<IActionResult> OnPost()
    {
	    ModelState.Remove($"{nameof(PurchaseRequest)}.{nameof(PurchaseRequest.DeliveryBuilding)}");
	    ModelState.Remove($"{nameof(PurchaseRequest)}.{nameof(PurchaseRequest.Date)}");
	    ModelState.Remove($"{nameof(PurchaseRequest)}.{nameof(PurchaseRequest.RequesterObjectId)}");
	    if (!ModelState.IsValid) return Page();

	    var requestingUser = await _activeDirectoryUserRepository.GetUserByEmail(RequesterEmailAddress);
	    if (requestingUser == null)
	    {
		    ModelState.AddModelError(nameof(RequesterEmailAddress), ErrorMessages.CouldNotRetrieveActiveDirectoryUser());
		    return Page();
	    }

	    PurchaseRequest.RequesterObjectId = requestingUser.Id;
        PurchaseRequest.Date = DateTime.Now;
        
        await PurchaseRequestRepository.Insert(PurchaseRequest);

        return RedirectToPage();
    }
}