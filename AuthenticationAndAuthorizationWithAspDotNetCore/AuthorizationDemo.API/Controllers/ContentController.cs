using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationDemo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        [Authorize(Policy = "Claim.Email")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Email based data");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult GetAdminContent()
        {
            return Ok("Admin Content");
        }
    }
}
