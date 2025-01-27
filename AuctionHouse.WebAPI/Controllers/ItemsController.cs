using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuctionHouse.WebAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase {
        private readonly AuctionHouseContext context;

        public ItemsController(AuctionHouseContext auctionContext) => context = auctionContext;


        [HttpGet]
        public ActionResult<IEnumerable<ItemDTO>> GetItems() {

            if (this.context.Items == null) {
                return BadRequest();
            }

            var items = context.Items
                            .Select(ItemDTO.itemToDTO)
                            .ToList();

            return Ok(items);
        }


        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ItemDTO>> GetItem(int id) {

            if (this.context.Items == null) {
                return BadRequest();
            }

            var item = context.Items.SingleOrDefault(i => i.Id == id);
            
            if (item == null) {
                return NotFound();
            }          

            return Ok(ItemDTO.itemToDTO(item));
        }

        [HttpGet("available")]
        public ActionResult<IEnumerable<ItemDTO>> GetItemsAvailable() {

            if (this.context.Items == null) {
                return BadRequest();
            }

            var itemsAvailable = context.Items
                            .Where(i => i.ItemStatus == ItemStatus.Available)
                            .Select(ItemDTO.itemToDTO)
                            .ToList();

            return Ok(itemsAvailable);
        }

        [HttpGet("sold")]
        public ActionResult<IEnumerable<ItemDTO>> GetItemsSold() {

            if (this.context.Items == null) {
                return BadRequest();
            }

            var itemsSold = context.Items
                            .Where(i => i.ItemStatus == ItemStatus.Sold)
                            .Select(ItemDTO.itemToDTO)
                            .ToList();

            return Ok(itemsSold);
        }


        [HttpPost]
        public ActionResult<Item> AddItem(Item item) {
            if (this.context.Items == null)
                return BadRequest();
                
            this.context.Items.Add(item);
            this.context.SaveChanges();

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }


        [HttpPut("{id}")]
        public ActionResult<Item> UpdateItem(int id, Item item) {
            if (this.context.Items == null)
                return BadRequest();


            if (id != item.Id) {
                return BadRequest();
            }

            this.context.Entry(item).State = EntityState.Modified;
            this.context.SaveChanges();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id) {
            if (this.context.Items == null)
                return BadRequest();

            if (!this.context.Items.Any(i => i.Id == id)) {
                return NotFound();
            }

            this.context.Items.Remove(new Item { Id = id });
            this.context.SaveChanges();

            return NoContent();
        }

    }
}
