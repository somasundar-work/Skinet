using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skinet.Application.Dtos;
using Skinet.Application.Extensions;
using Skinet.Application.Interfaces;
using Skinet.Application.Services;
using Skinet.Application.Specifications;
using Skinet.Entities.Delivery;
using Skinet.Entities.OrderAggregate;
using Skinet.Entities.Product;

namespace Skinet.API.Controllers
{
    [Authorize]
    public class OrdersController(ICartService cartService, IUnitOfWork unit) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            var email = User.GetEmail();
            var cart = await cartService.GetCartAsync(orderDto.CartId);

            if (cart == null)
                return BadRequest("Cart not found");

            if (cart.PaymentIntentId == null)
                return BadRequest("No payment intent for this order");

            var items = new List<OrderItem>();

            foreach (var item in cart.Items)
            {
                var productItem = await unit.Repository<Product>().GetByIdAsync(item.ProductId);
                if (productItem == null)
                    return BadRequest("problem with the order");

                var itemOrdered = new ProductItemOrdered
                {
                    ProductId = item.ProductId,
                    Name = item.ProductName,
                    Brand = productItem.Brand,
                    Category = productItem.Category,
                    PictureUrl = productItem.PictureUrl,
                    Description = productItem.Description,
                };

                var orderItem = new OrderItem
                {
                    ItemOrdered = itemOrdered,
                    Price = item.Price,
                    Quantity = item.Quantity,
                };
                items.Add(orderItem);
            }

            var deliveryMethod = await unit.Repository<DeliveryMethod>()
                .GetByIdAsync(orderDto.DeliveryMethodId);

            if (deliveryMethod == null)
                return BadRequest("No delivery method selected");

            var order = new Order
            {
                OrderItems = items,
                DeliveryMethod = deliveryMethod,
                ShippingAddress = orderDto.ShippingAddress,
                SubTotal = items.Sum(x => x.Price * x.Quantity),
                PaymentSummary = orderDto.PaymentSummary,
                PaymentIntentId = cart.PaymentIntentId,
                BuyerEmail = email,
                Discount = orderDto.Discount,
                CouponCode = orderDto.CouponCode,
            };

            unit.Repository<Order>().Add(order);
            if (await unit.Complete())
            {
                return Ok(order);
            }
            return BadRequest("Problem creating order");
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var spec = new OrderSpecification(User.GetEmail());

            var orders = await unit.Repository<Order>().ListAsync(spec);

            var ordersToReturn = orders.Select(o => o.ToDto()).ToList();

            return Ok(ordersToReturn);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var spec = new OrderSpecification(User.GetEmail(), id);

            var order = await unit.Repository<Order>().GetEntityWithSpec(spec);

            if (order == null)
                return NotFound();

            return order.ToDto();
        }
    }
}
