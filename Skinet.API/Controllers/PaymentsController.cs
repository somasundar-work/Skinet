using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Skinet.Application.Extensions;
using Skinet.Application.Interfaces;
using Skinet.Application.Services;
using Skinet.Application.SignalR;
using Skinet.Application.Specifications;
using Skinet.Entities.Cart;
using Skinet.Entities.Delivery;
using Skinet.Entities.OrderAggregate;
using Stripe;

namespace Skinet.API.Controllers
{
    public class PaymentsController(
        IPaymentService paymentService,
        IUnitOfWork unit,
        ILogger<PaymentsController> logger,
        IHubContext<NotificationHub> hubContext,
        IConfiguration configuration
    ) : BaseApiController
    {
        private readonly string _whSecret = configuration["StripeSettings:whSecret"] ?? "";

        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
        {
            var cart = await paymentService.CreateOrUpdatePaymentIntent(cartId);
            if (cart == null)
                return BadRequest("Problem with your cart");

            return Ok(cart);
        }

        [Authorize]
        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await unit.Repository<DeliveryMethod>().ListAllAsync());
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = ConstructStripeEvent(json);

                if (stripeEvent.Data.Object is not PaymentIntent intent)
                    return BadRequest("Invalid event data");

                await HandlePaymentIntentSucceeded(intent);

                return Ok();
            }
            catch (StripeException ex)
            {
                logger.LogError(ex, "Stripe webhook error");
                return StatusCode(StatusCodes.Status500InternalServerError, "Stripe webhook error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred"
                );
            }
        }

        private async Task HandlePaymentIntentSucceeded(PaymentIntent intent)
        {
            if (intent.Status == "succeeded")
            {
                var spec = new OrderSpecification(intent.Id, true);

                var order =
                    await unit.Repository<Order>().GetEntityWithSpec(spec)
                    ?? throw new Exception("Order not found");

                if (((long)order.GetTotal() * 100) != intent.Amount)
                {
                    order.Status = OrderStatus.PaymentMisMatch;
                }
                else
                {
                    order.Status = OrderStatus.PaymentReceived;
                }

                await unit.Complete();

                var connectionId = NotificationHub.GetConnectionIdByEmail(order.BuyerEmail);

                if (!string.IsNullOrEmpty(connectionId))
                {
                    await hubContext
                        .Clients.Client(connectionId)
                        .SendAsync("OrderCompleteNotification", order.ToDto());
                }
            }
        }

        private Event ConstructStripeEvent(string json)
        {
            try
            {
                return EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _whSecret
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to construct");
                throw new StripeException("invalid signal");
            }
        }
    }
}
