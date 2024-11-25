using eCommerce.Application.DTOs.Card;
using eCommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentMethodService paymentMethodService) : ControllerBase
    {
        [HttpGet("PaymentMethods")]
        public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPaymentMethods()
        {
            var methods = await paymentMethodService.GetPaymentMethods();
            if (methods == null) return NotFound();
            return Ok(methods);
        }
    }
}
