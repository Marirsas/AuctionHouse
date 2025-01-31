using AuctionHouse.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.WebAPI.Data {
    /// <summary>
    /// Represents the database context for the Auction House application.
    /// </summary>
    public class AuctionHouseContext : DbContext {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionHouseContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public AuctionHouseContext(DbContextOptions<AuctionHouseContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the Items table.
        /// </summary>
        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the Categories table.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the Sales table.
        /// </summary>
        public DbSet<Sale> Sales { get; set; }
    }
}
