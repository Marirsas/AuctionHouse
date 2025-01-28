using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuctionHouse.WebAPI.DTO;
using Microsoft.EntityFrameworkCore;
using AuctionHouse.WebAPI.Services;

namespace AuctionHouse.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase {
        private readonly IItem itemService;


        public ItemsController(IItem item) {
            itemService = item;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ItemDTO>> GetItems() {

            if (itemService.GetItems() == null) {
                return BadRequest();
            }

            return Ok(itemService.GetItems());
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


        [HttpPost]
        public ActionResult<ItemDTO> AddItem(ItemDTO item) {
            try {
                itemService.AddItem(item);

                if (itemService.AddItem(item) == null) {
                    return BadRequest();
                }
            }
            catch (ArgumentNullException e) {
                return BadRequest(e.Message);
            }




            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }


        [HttpPut("{id}")]
        public ActionResult<Item> UpdateItem(int id, Item item) {
            if (this.context.Items == null)
                return BadRequest();


            if (id != item.Id) {
                return BadRequest();
            }

            var itemTemp = context.Items.SingleOrDefault(i => i.Id == id);
            if (itemTemp.ItemStatus == ItemStatus.Sold) {
                return BadRequest(itemTemp.ItemStatus.ToString());
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
