using System.ComponentModel.DataAnnotations;

namespace AuctionHouse.WebAPI.Models {
    /// <summary>
    /// Represents a category in the auction house.
    /// </summary>
    public class Category {

        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [MaxLength(100)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the category.
        /// </summary>
        [MaxLength(150)]
        public string? Description { get; set; }
    }
}
