using AuctionHouse.WebAPI.Models;

namespace AuctionHouse.WebAPI.DTO {
    /// <summary>
    /// Data Transfer Object for Item.
    /// </summary>
    public class ItemDTO {
        /// <summary>
        /// Gets or sets the item ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the item description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the initial price of the item.
        /// </summary>
        public decimal InitialPrice { get; set; }

        /// <summary>
        /// Gets or sets the category ID of the item.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the status of the item.
        /// </summary>
        public ItemStatus ItemStatus { get; set; }
    }
}

