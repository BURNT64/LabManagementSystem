using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Models;
using LabManagementSystem.Data.Repositories.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Orders;

public class UpdateModel : PageModel
{
    [BindProperty]
    public OrderModel Order { get; set; }

    private readonly IOrderRepository _orderRepository;

    public UpdateModel(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<IActionResult> OnGet(int? requestId)
    {
        if (requestId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("requestId"));
        }

        var order = await _orderRepository.Select((int) requestId);
        if (order == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("order"));
        }

        Order = order;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Order)}.{nameof(Order.PurchaseRequest)}");
        if (!ModelState.IsValid) return Page();

        await _orderRepository.Update(Order);

        return RedirectToPage("/Admin/Orders/Index");
    }
    
    public async Task<IActionResult> OnPostDelete(int? requestId)
    {
        if (requestId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("requestId"));
        }

        await _orderRepository.Delete((int) requestId);

        return RedirectToPage("/Admin/Orders/Index");
    }
}