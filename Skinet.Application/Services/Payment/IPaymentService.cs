using System;
using Skinet.Entities.Cart;

namespace Skinet.Application.Services;

public interface IPaymentService
{
    Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId);
}
