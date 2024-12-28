using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.Application.Services.Coupon;
using Skinet.Entities.OrderAggregate;

namespace Skinet.API.Controllers
{
    public class CouponController(ICouponService couponService) : BaseApiController
    {
        [HttpGet("{code}")]
        public async Task<ActionResult<AppCoupon>> ValidateCoupon(string code)
        {
            var coupon = await couponService.GetCouponFromPromoCode(code);
            if (coupon == null)
                return BadRequest("Invalid voucher code");
            return coupon;
        }
    }
}
