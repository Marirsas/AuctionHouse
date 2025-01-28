using AuctionHouse.WebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuctionHouse.WebAPI.DTO {
    public class SaleDTO {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public DateOnly DateOfSale { get; set; }

        
        public decimal SalePrice { get; set; }
    }
}
