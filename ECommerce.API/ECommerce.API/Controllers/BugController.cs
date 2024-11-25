using ECommerce.API.Errors;
using ECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController(ECommerceDbContext context) : BaseApiController
    {
        [HttpGet("NotFound")]
        public IActionResult GetNotFoundRequest() 
        {
            var thing = context.Products.Find(50);
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(thing);
        }
        [HttpGet("Server Error")]
        public IActionResult GetServerError()
        {
            var thing = context.Products.Find(50);
            var returnThing= thing.ToString();

            return Ok();
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
