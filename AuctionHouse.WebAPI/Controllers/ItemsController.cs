using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuctionHouse.WebAPI.DTO;
using Microsoft.EntityFrameworkCore;
using AuctionHouse.WebAPI.Services.Interfaces;

namespace AuctionHouse.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase {

        private readonly IItemsService itemService;

        public ItemsController(IItemsService itemService) => this.itemService = itemService;

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>A list of ItemDTO objects.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ItemDTO>> GetItems() {
            if (itemService.GetItems() == null) {
                return BadRequest();
            }
            return Ok(itemService.GetItems());
        }

        /// <summary>
        /// Retrieves all available items.
        /// </summary>
        /// <returns>A list of available ItemDTO objects.</returns>
        [HttpGet("available")]
        public ActionResult<IEnumerable<ItemDTO>> GetItemsAvailable() {
            if (itemService.GetItemsAvailable() == null) {
                return BadRequest();
            }
            return Ok(itemService.GetItemsAvailable());
        }

        /// <summary>
        /// Retrieves all sold items.
        /// </summary>
        /// <returns>A list of sold ItemDTO objects.</returns>
        [HttpGet("sold")]
        public ActionResult<IEnumerable<ItemDTO>> GetItemsSold() {
            if (itemService.GetItemsSold() == null) {
                return BadRequest();
            }
            return Ok(itemService.GetItemsSold());
        }

        /// <summary>
        /// Retrieves items by category.
        /// </summary>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>A list of ItemDTO objects in the specified category.</returns>
        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<ItemDTO>> GetItemsByCategory(int categoryId) {
            try {
                if (itemService.GetItemsByCategory(categoryId) == null) {
                    return BadRequest();
                }
                return Ok(itemService.GetItemsByCategory(categoryId));
            }
            catch (InvalidOperationException e) {
                return NotFound(e.Message);
            }
            catch (ArgumentException) {
                return NoContent();
            }
        }

        /// <summary>
        /// Retrieves an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>An ItemDTO object.</returns>
        [HttpGet("{id}")]
        public ActionResult<ItemDTO> GetItem(int id) {
            try {
                var item = itemService.GetItem(id);

                if (item == null) {
                    return BadRequest();
                }
                return Ok(item);
            }
            catch (ArgumentNullException e) {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="item">The ItemDTO object to add.</param>
        /// <returns>The created ItemDTO object.</returns>
        [HttpPost]
        public ActionResult<ItemDTO> AddItem(ItemDTO item) {
            if (item == null) {
                return BadRequest();
            }

            try {
                var newItem = itemService.AddItem(item);
                return CreatedAtAction(nameof(GetItem), new { id = newItem }, newItem);
            }
            catch (ArgumentOutOfRangeException e) {
                return BadRequest(e.Message);
            }
            catch (ArgumentException f) {
                return BadRequest(f.Message);
            }
            catch (InvalidOperationException g) {
                return BadRequest(g.Message);
            }
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="itemDTO">The updated ItemDTO object.</param>
        /// <returns>An ActionResult.</returns>
        [HttpPut("{id}")]
        public ActionResult<Item> UpdateItem(int id, ItemDTO itemDTO) {
            if (itemService.UpdateItem(id, itemDTO) == null) {
                return BadRequest();
            }

            try {
                itemService.UpdateItem(id, itemDTO);
                return Ok();
            }
            catch (ArgumentNullException e) {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException f) {
                return BadRequest(f.Message);
            }
            catch (ArgumentException g) {
                return BadRequest(g.Message);
            }
        }

        /// <summary>
        /// Deletes an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>An IActionResult.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id) {
            if (itemService.RemoveItem(id) == null) {
                return BadRequest();
            }

            itemService.RemoveItem(id);
            return NoContent();
        }
    }
}
