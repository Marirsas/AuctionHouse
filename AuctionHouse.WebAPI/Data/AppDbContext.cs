using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.WebAPI.Data {
    /// <summary>
    /// Represents the database context for the AuctionHouse application, 
    /// extending the IdentityDbContext to include IdentityUser for authentication and authorization.
    /// </summary>
    public class AppDbContext : IdentityDbContext<IdentityUser> {
        /// <summary>
        /// Initializes a new instance of the AppDbContext class using the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public AppDbContext(DbContextOptions options) : base(options) {
        }
    }
}
