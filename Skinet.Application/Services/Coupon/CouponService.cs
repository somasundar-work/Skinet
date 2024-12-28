using System;
using Microsoft.Extensions.Configuration;
using Skinet.Entities.OrderAggregate;
using Stripe;

namespace Skinet.Application.Services.Coupon;

public class CouponService : ICouponService
{
    public CouponService(IConfiguration config)
    {
        StripeConfiguration.ApiKey = config[$"StripeSettings:Secretkey"];
    }

    public async Task<AppCoupon?> GetCouponFromPromoCode(string code)
    {
        var promotionService = new PromotionCodeService();
        var options = new PromotionCodeListOptions { Code = code };
        var promotionCodes = await promotionService.ListAsync(options);
        var promotionCode = promotionCodes.FirstOrDefault();
        if (promotionCode != null && promotionCode.Coupon != null)
        {
            return new AppCoupon
            {
                Name = promotionCode.Coupon.Name,
                AmountOff = promotionCode.Coupon.AmountOff / 100,
                PercentOff = promotionCode.Coupon.PercentOff,
                CouponId = promotionCode.Coupon.Id,
                PromotionCode = promotionCode.Code,
            };
        }
        return null;
    }
}
