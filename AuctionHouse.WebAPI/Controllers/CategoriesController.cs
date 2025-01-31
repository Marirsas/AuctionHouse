using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AuctionHouse.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {

        private readonly ICategoriesService categoryService;
        public CategoriesController(ICategoriesService categoryService) => this.categoryService = categoryService;


        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories() {

            if (categoryService.GetCategories() == null) {
                return BadRequest();
            }

            return Ok(categoryService.GetCategories());

        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategory(int id) {

            try {
                var category = categoryService.GetCategory(id);

                if (category == null) {
                    return BadRequest();
                }

                return Ok(category);
            }
            catch (ArgumentNullException e) {

                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<CategoryDTO> AddCategory(CategoryDTO categoryDTO) {

            if (categoryDTO == null) {
                return BadRequest();
            }

            var newCategory = categoryService.AddCategory(categoryDTO);

            return CreatedAtAction(nameof(GetCategory), new { id = newCategory }, newCategory);
        }

        [HttpPut("{id}")]
        public ActionResult<CategoryDTO> UpdateCategory(int id, CategoryDTO categoryDTO) {

            try {
                if (categoryService.UpdateCategory(id, categoryDTO) == null) {
                    return BadRequest();
                }
                return Ok(categoryService.UpdateCategory(id, categoryDTO));
            }
            catch (ArgumentNullException e) {
                return BadRequest(e.Message);
            }
            catch (ArgumentException g) {
                return NotFound(g.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveCategory(int id) {
            try {if (categoryService.RemoveCategory(id) == null) {
                return BadRequest();
            }

            categoryService.RemoveCategory(id);
            return NoContent();

            }catch(ArgumentException e) {
                return NotFound(e.Message);
            }            
        }
    }
}
