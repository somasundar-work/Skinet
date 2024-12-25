using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skinet.Application.Interfaces;
using Skinet.Application.Services;
using Skinet.Entities.Cart;
using Skinet.Entities.Delivery;

namespace Skinet.API.Controllers
{
    [Authorize]
    public class PaymentsController(
        IPaymentService paymentService,
        IGenericRepository<DeliveryMethod> dmRepo
    ) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
        {
            var cart = await paymentService.CreateOrUpdatePaymentIntent(cartId);
            if (cart == null)
                return BadRequest("Problem with your cart");

            return Ok(cart);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await dmRepo.ListAllAsync());
        }
    }
}
