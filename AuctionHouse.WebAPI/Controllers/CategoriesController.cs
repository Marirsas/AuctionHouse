using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly AuctionHouseContext context;
        public CategoriesController(AuctionHouseContext auctionContext) => context = auctionContext;

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories() {

            if (this.context.Categories == null) {
                return BadRequest();
            }

            var categories = context.Categories
                            .Select(CategoryDTO.categoryToDTO
                            )
                            .ToList();

            return Ok(categories);
        }

        [HttpGet("wItems")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoriesWithItems() {

            if (this.context.Categories == null) {
                return BadRequest();
            }

            var categories = context.Categories.Include(i=> i.Items)
                            .Select(CategoryDTO.categoryToDTO
                            )
                            .ToList();

            return Ok(categories);
        }

        [HttpGet("wItems/{id}")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoryWithItems(int id) {

            if (this.context.Categories == null) {
                return BadRequest();
            }

            var category = context.Categories.Include(i=> i.Items).SingleOrDefault(c => c.Id == id);

            return Ok(CategoryDTO.categoryToDTO(category));
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategory(int id) {

            if (this.context.Categories == null) {
                return BadRequest();
            }

            var category = context.Categories.SingleOrDefault(c => c.Id == id); 
            
            if (category == null) {
                return NotFound();
            }

            return Ok(CategoryDTO.categoryToDTO(category));
        }

        [HttpPost]
        public ActionResult<Category> AddCategory(Category cat) {
            if (this.context.Categories == null)
                return BadRequest();

            this.context.Categories.Add(cat);
            this.context.SaveChanges();

            return CreatedAtAction(nameof(AddCategory), new { id = cat.Id }, cat);
        }


        [HttpPut("{id}")]
        public ActionResult<Category> UpdateCategory(int id, Category cat) {
            if (this.context.Categories == null)
                return BadRequest();


            if (id != cat.Id) {
                return BadRequest();
            }
            

            this.context.Entry(cat).State = EntityState.Modified;
            this.context.SaveChanges();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) {
            if (this.context.Categories == null)
                return BadRequest();

            if (!this.context.Categories.Any(i => i.Id == id)) {
                return NotFound();
            }

            this.context.Categories.Remove(new Category { Id = id });
            this.context.SaveChanges();

            return NoContent();
        }



    }
}
