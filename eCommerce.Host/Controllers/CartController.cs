using eCommerce.Application.DTOs.Card;
using eCommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpPost("Chechout")]
        public async Task <IActionResult> Checkout(Checkout checkout)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await cartService.Checkout(checkout);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [HttpPost("SaveChechout")]
        public async Task<IActionResult> SaveCheckout(IEnumerable<CreateAchieve> achieves)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await cartService.SaveChecoutHistory(achieves);
            return result.Success ? Ok(result) : BadRequest(result);

        }
    }
}
