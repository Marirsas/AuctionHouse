using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.WebAPI.Controllers {
    /// <summary>
    /// Controller to handle user-related actions.
    /// </summary>
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        /// <summary>
        /// Gets a message indicating access to the User controller.
        /// </summary>
        /// <returns>A message indicating access to the User controller.</returns>
        [HttpGet]
        public IActionResult Get() {
            return Ok("You have accessed the User controller.");
        }
    }
}
