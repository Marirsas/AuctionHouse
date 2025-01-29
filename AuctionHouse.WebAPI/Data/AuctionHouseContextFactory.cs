namespace AuctionHouse.WebAPI.Data {
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public class AuctionHouseContextFactory : IDesignTimeDbContextFactory<AuctionHouseContext> {
        public AuctionHouseContext CreateDbContext(string[] args) {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AuctionHouseContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AuctionHouseDB"));

            return new AuctionHouseContext(optionsBuilder.Options);
        }
    }
}
