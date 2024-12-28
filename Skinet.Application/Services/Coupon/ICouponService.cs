using System;
using Skinet.Entities.OrderAggregate;

namespace Skinet.Application.Services.Coupon;

public interface ICouponService
{
    Task<AppCoupon?> GetCouponFromPromoCode(string code);
}
