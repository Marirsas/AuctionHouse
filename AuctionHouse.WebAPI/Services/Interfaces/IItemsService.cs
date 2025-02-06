using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;

namespace AuctionHouse.WebAPI.Services.Interfaces
{
    /// <summary>
    /// Interface for item service operations.
    /// </summary>
    public interface IItemsService {
        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns>A collection of item DTOs.</returns>
        IEnumerable<ItemDTO> GetItems();

        /// <summary>
        /// Gets an item by ID.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <returns>The item DTO.</returns>
        ItemDTO GetItem(int id);

        /// <summary>
        /// Gets all available items.
        /// </summary>
        /// <returns>A collection of available item DTOs.</returns>
        IEnumerable<ItemDTO> GetItemsAvailable();

        /// <summary>
        /// Gets all sold items.
        /// </summary>
        /// <returns>A collection of sold item DTOs.</returns>
        IEnumerable<ItemDTO> GetItemsSold();

        /// <summary>
        /// Gets items by category ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>A collection of item DTOs in the specified category.</returns>
        IEnumerable<ItemDTO> GetItemsByCategory(int id);

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="item">The item DTO to add.</param>
        /// <returns>The added item DTO.</returns>
        ItemDTO AddItem(ItemDTO item);

        /// <summary>
        /// Removes an item by ID.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <returns>The removed item DTO.</returns>
        ItemDTO RemoveItem(int id);

        /// <summary>
        /// Updates an item by ID.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <param name="itemDTO">The item DTO with updated information.</param>
        /// <returns>The updated item DTO.</returns>
        ItemDTO UpdateItem(int id, ItemDTO itemDTO);

        /// <summary>
        /// Updates the status of an item by ID.
        /// </summary>
        /// <param name="itemId">The item ID.</param>
        /// <returns>True if the status was updated successfully, otherwise false.</returns>
        Boolean UpdateItemStatus(int itemId, ItemStatus itemStatus);
    }
}
