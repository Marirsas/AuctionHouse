using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuctionHouse.WebAPI.DTO;
using Microsoft.EntityFrameworkCore;
using AuctionHouse.WebAPI.Services.Interfaces;

namespace AuctionHouse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase {


        private readonly IItemsService itemService;

        public ItemsController(IItemsService itemService) => this.itemService = itemService;


        [HttpGet]
        public ActionResult<IEnumerable<ItemDTO>> GetItems() {

            if (itemService.GetItems() == null) {
                return BadRequest();
            }

            return Ok(itemService.GetItems());
        }

        [HttpGet("available")]
        public ActionResult<IEnumerable<ItemDTO>> GetItemsAvailable() {

            if (itemService.GetItemsAvailable() == null) {
                return BadRequest();
            }

            return Ok(itemService.GetItemsAvailable());
        }

        [HttpGet("sold")]
        public ActionResult<IEnumerable<ItemDTO>> GetItemsSold() {

            if (itemService.GetItemsSold() == null) {
                return BadRequest();
            }

            return Ok(itemService.GetItemsSold());
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<ItemDTO>> GetItemsByCategory(int categoryId) {

            if (itemService.GetItemsByCategory(categoryId) == null) {
                return BadRequest();
            }

            return Ok(itemService.GetItemsByCategory(categoryId));
        }

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

        [HttpPost]
        public ActionResult<ItemDTO> AddItem(ItemDTO item) {

            if (item == null) {
                return BadRequest();
            }

            try {
                itemService.AddItem(item);
                return CreatedAtAction(nameof(AddItem), new { id = item.Id }, item);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id) {
            if (itemService.RemoveItem(id) == null) {
                return BadRequest();
            }

            try {
                itemService.RemoveItem(id);
                return NoContent();
            } catch (ArgumentException e) {
                return NotFound(e.Message);
            }
                        
        }

    }
}
