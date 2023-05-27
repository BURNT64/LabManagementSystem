using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.API.Errors;
using LabManagementSystem.Data.Repositories.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabManagementSystem.Pages.Admin.Orders;

public class IndexModel : PageModel
{
    public readonly IOrderRepository OrderRepository;
    
    public readonly IActiveDirectoryUserRepository ActiveDirectoryUserRepository;

    public IndexModel(
        IOrderRepository orderRepository,
        IActiveDirectoryUserRepository activeDirectoryUserRepository
    )
    {
        OrderRepository = orderRepository;
        ActiveDirectoryUserRepository = activeDirectoryUserRepository;
    }

    public async Task<IActionResult> OnPostMarkDelivered(int? requestId)
    {
        if (requestId == null)
        {
            return BadRequest(ErrorMessages.HttpParameterNotProvided("requestId"));
        }
        
        var order = await OrderRepository.Select((int) requestId);
        if (order == null)
        {
            return NotFound(ErrorMessages.CouldNotRetrieveFromDatabase("order"));
        }

        order.IsDelivered = true;

        await OrderRepository.Update(order);

        return Page();
    }
}