using Microsoft.Extensions.Configuration;
using Skinet.Application.Interfaces;
using Skinet.Entities.Cart;
using Skinet.Entities.Delivery;
using Stripe;

namespace Skinet.Application.Services;

public class PaymentService(
    IConfiguration config,
    ICartService cartService,
    IGenericRepository<Entities.Product.Product> productRepo,
    IGenericRepository<DeliveryMethod> dmRepo
) : IPaymentService
{
    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
    {
        var cart = await cartService.GetCartAsync(cartId);
        if (cart == null)
            return null;

        StripeConfiguration.ApiKey = config[$"StripeSettings:{cart.Currency}:Secretkey"];
        long shippingPrice = 0;
        if (cart.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await dmRepo.GetByIdAsync((int)cart.DeliveryMethodId);
            if (deliveryMethod == null)
                return null;
            shippingPrice = (long)deliveryMethod.Price * 100;
        }

        foreach (var item in cart.Items)
        {
            var productItem = await productRepo.GetByIdAsync(item.ProductId);
            if (productItem == null)
                return null;
            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }

        var service = new PaymentIntentService();
        PaymentIntent? intent = null;

        if (string.IsNullOrEmpty(cart.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * x.Price * 100) + shippingPrice,
                Currency = cart.Currency,
                PaymentMethodTypes = ["card"],
            };
            intent = await service.CreateAsync(options);
            cart.PaymentIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * x.Price * 100) + shippingPrice,
            };
            intent = await service.UpdateAsync(cart.PaymentIntentId, options);
            cart.PaymentIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }

        return await cartService.SetCartAsync(cart);
    }
}
