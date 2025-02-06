namespace AuctionHouse.WebAPI.Models {
    /// <summary>
    /// Represents the status of an item in the auction house.
    /// </summary>
    public enum ItemStatus {
        /// <summary>
        /// The item is available for auction.
        /// </summary>
        Available = 0,

        /// <summary>
        /// The item is in auction process.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// The item has been sold.
        /// </summary>
        Sold = 2
    }
}
