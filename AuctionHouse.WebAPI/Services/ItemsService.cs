using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AuctionHouse.WebAPI.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace AuctionHouse.WebAPI.Services
{
    /// <summary>
    /// Service for managing items in the auction house.
    /// </summary>
    public class ItemsService : IItemsService {
        private readonly AuctionHouseContext context;
        private readonly IMapper mapper;
        private readonly ICategoriesService categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsService"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="auctionContext">The auction house database context.</param>
        /// <param name="categoryService">The category service instance.</param>
        public ItemsService(IMapper mapper, AuctionHouseContext auctionContext, ICategoriesService categoryService) {
            this.mapper = mapper;
            context = auctionContext;
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns>A collection of item DTOs.</returns>
        public IEnumerable<ItemDTO> GetItems() {
            if (this.context.Items == null) {
                return null;
            }
            return context.Items.Select(item => mapper.Map<ItemDTO>(item)).ToList();
        }

        /// <summary>
        /// Gets all available items.
        /// </summary>
        /// <returns>A collection of available item DTOs.</returns>
        public IEnumerable<ItemDTO> GetItemsAvailable() {
            if (this.context.Items == null) {
                return null;
            }
            return context.Items
                          .Where(i => i.ItemStatus == ItemStatus.Available)
                          .Select(item => mapper.Map<ItemDTO>(item))
                          .ToList();
        }

        /// <summary>
        /// Gets all sold items.
        /// </summary>
        /// <returns>A collection of sold item DTOs.</returns>
        public IEnumerable<ItemDTO> GetItemsSold() {
            if (this.context.Items == null) {
                return null;
            }
            return context.Items
                          .Where(i => i.ItemStatus == ItemStatus.Sold)
                          .Select(item => mapper.Map<ItemDTO>(item))
                          .ToList();
        }

        /// <summary>
        /// Gets items by category ID.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns>A collection of item DTOs in the specified category.</returns>
        public IEnumerable<ItemDTO> GetItemsByCategory(int categoryId) {
            if (this.context.Items == null) {
                return null;
            }
            if (!context.Categories.Any(c => c.Id == categoryId)) {
                throw new InvalidOperationException("The category doesn't exists!");
            }
            var itemsList = context.Items
                          .Where(i => i.CategoryId == categoryId)
                          .Select(item => mapper.Map<ItemDTO>(item))
                          .ToList();
            if (itemsList.IsNullOrEmpty()) {
                throw new ArgumentException();
            }
            return itemsList;
        }

        /// <summary>
        /// Gets an item by ID.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <returns>The item DTO.</returns>
        public ItemDTO GetItem(int id) {
            var item = mapper.Map<ItemDTO>(context.Items.SingleOrDefault(i => i.Id == id));
            if (context.Items == null) {
                return null;
            }
            if (item == null) {
                throw new ArgumentNullException("Item not found!");
            }
            return item;
        }

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="itemDTO">The item DTO to add.</param>
        /// <returns>The added item DTO.</returns>
        public ItemDTO AddItem(ItemDTO itemDTO) {
            if (itemDTO.ItemStatus == ItemStatus.Sold) {
                throw new InvalidOperationException("Unable to add an item with sold status!");
            }
            if (itemDTO.InitialPrice < 1) {
                throw new ArgumentOutOfRangeException("The price must be higher than 0");
            }
            var category = context.Categories.SingleOrDefault(c => c.Id == itemDTO.CategoryId);
            if (category == null) {
                throw new ArgumentException("Category doesn't exists!");
            }
            var itemTemp = new Item {
                Name = itemDTO.Name,
                Description = itemDTO.Description,
                InitialPrice = itemDTO.InitialPrice,
                ItemStatus = itemDTO.ItemStatus,
                CategoryId = itemDTO.CategoryId,
                Category = category
            };
            this.context.Items.Add(itemTemp);
            this.context.SaveChanges();
            return mapper.Map<ItemDTO>(itemTemp);
        }

        /// <summary>
        /// Removes an item by ID.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <returns>The removed item DTO.</returns>
        public ItemDTO RemoveItem(int id) {
            if (this.context.Items == null) {
                return null;
            }
            var item = context.Items.SingleOrDefault(i => i.Id == id);
            if (item == null) {
                return null;
            }
            this.context.Items.Remove(item);
            this.context.SaveChanges();
            return mapper.Map<ItemDTO>(item);
        }

        /// <summary>
        /// Updates an item by ID.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <param name="itemDTO">The item DTO with updated information.</param>
        /// <returns>The updated item DTO.</returns>
        public ItemDTO UpdateItem(int id, ItemDTO itemDTO) {
            if (this.context.Items == null) {
                return null;
            }
            if (id != itemDTO.Id) {
                throw new ArgumentException("The ID provided doesn't match the ID of the item to be updated");
            }
            var itemToCheckStatus = context.Items.SingleOrDefault(i => i.Id == id);
            if (itemToCheckStatus.ItemStatus == ItemStatus.Sold || itemToCheckStatus == null) {
                throw new InvalidOperationException("Items with sold status can't be updated");
            }
            var category = context.Categories.SingleOrDefault(c => c.Id == itemDTO.CategoryId);
            if (category == null) {
                throw new ArgumentNullException("Category doesn't exists!");
            }
            mapper.Map(itemDTO, itemToCheckStatus);
            this.context.Entry(itemToCheckStatus).State = EntityState.Modified;
            this.context.SaveChanges();
            return mapper.Map<ItemDTO>(itemToCheckStatus);
        }

        /// <summary>
        /// Updates the status of an item by ID.
        /// </summary>
        /// <param name="itemId">The item ID.</param>
        /// <returns>True if the status was updated successfully, otherwise false.</returns>
        public bool UpdateItemStatus(int itemId) {
            var item = context.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null) {
                throw new ArgumentNullException("Item not found!");
            }
            if (item.ItemStatus == ItemStatus.Sold) {
                throw new InvalidOperationException("Item already sold");
            }
            item.ItemStatus = ItemStatus.Sold;
            context.SaveChanges();
            return true;
        }
    }
}
