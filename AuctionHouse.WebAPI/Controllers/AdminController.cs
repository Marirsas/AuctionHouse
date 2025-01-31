using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.WebAPI.Controllers {
    /// <summary>
    /// Controller for admin-specific actions.
    /// Only accessible by users with the "Admin" role.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase {
        /// <summary>
        /// GET: api/admin
        /// Returns a message indicating access to the Admin controller.
        /// </summary>
        /// <returns>A message indicating access to the Admin controller.</returns>
        [HttpGet]
        public IActionResult Get() {
            return Ok("You have accessed the Admin controller.");
        }
    }
}
