using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionHouse.WebAPI.Models {
    /// <summary>
    /// Represents a sale in the auction house.
    /// </summary>
    public class Sale {

        /// <summary>
        /// Gets or sets the unique identifier for the sale.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the item being sold.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets the item being sold.
        /// </summary>
        public Item? Item { get; set; }

        /// <summary>
        /// Gets or sets the date of the sale.
        /// </summary>
        public DateOnly? DateOfSale { get; set; }

        /// <summary>
        /// Gets or sets the sale price.
        /// </summary>
        [Column(TypeName = "decimal(15, 2)")]
        public decimal SalePrice { get; set; }
    }
}
