using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionHouse.WebAPI.Models {
    /// <summary>
    /// Represents an item in the auction house.
    /// </summary>
    public class Item {
        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        [MaxLength(100)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        [MaxLength(150)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the initial price of the item.
        /// </summary>
        [Column(TypeName = "decimal(10, 2)")]
        public decimal InitialPrice { get; set; }

        /// <summary>
        /// Gets or sets the category of the item.
        /// </summary>
        public Category? Category { get; set; }

        /// <summary>
        /// Gets or sets the category identifier for the item.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the status of the item.
        /// </summary>
        public ItemStatus ItemStatus { get; set; }
    }
}
