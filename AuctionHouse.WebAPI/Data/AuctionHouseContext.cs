using AuctionHouse.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.WebAPI.Data {
    public class AuctionHouseContext : DbContext {
        public AuctionHouseContext(DbContextOptions<AuctionHouseContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
