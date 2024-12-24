using Microsoft.AspNetCore.Mvc;
using Skinet.Application.Services;
using Skinet.Entities.Cart;

namespace Skinet.API.Controllers
{
    public class CartController(ICartService cartService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetAsync([FromQuery] string Id)
        {
            var cart = await cartService.GetCartAsync(Id);

            return Ok(cart ?? new ShoppingCart { Id = Id });
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> PostAsync([FromBody] ShoppingCart cart)
        {
            var updatedCart = await cartService.SetCartAsync(cart);
            if (updatedCart == null)
                return BadRequest("Problem with cart");
            return updatedCart;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromQuery] string id)
        {
            var result = await cartService.DeleteCartAsync(id);
            if (!result)
                return BadRequest("Problem deleting cart");
            return Ok();
        }
    }
}
