using AuctionHouse.WebAPI.Models;

namespace AuctionHouse.WebAPI.DTO {
    /// <summary>
    /// Data Transfer Object for Category.
    /// </summary>
    public class CategoryDTO {
        /// <summary>
        /// Gets or sets the Id of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description of the category.
        /// </summary>
        public string Description { get; set; }
    }
}
