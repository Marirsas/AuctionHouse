using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionHouse.WebAPI.Models {
    public class Sale {
        [Key]
        public int Id { get; set; }


        public int ItemId { get; set; }


        public Item? Item { get; set; }


        public DateOnly DateOfSale { get; set; }


        [Column(TypeName = "decimal(15, 2)")]
        public decimal SalePrice { get; set; }


    }
}
