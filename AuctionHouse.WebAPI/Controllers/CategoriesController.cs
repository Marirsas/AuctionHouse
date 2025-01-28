using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AuctionHouse.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.WebAPI.Controllers
{
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

            categoryService.AddCategory(categoryDTO);

            return CreatedAtAction(nameof(AddCategory), new { id = categoryDTO.Id }, categoryDTO);
        }

        [HttpPut("{id}")]
        public ActionResult<CategoryDTO> UpdateCategory(int id, CategoryDTO categoryDTO) {

            if (categoryService.UpdateCategory(id, categoryDTO) == null) {
                return BadRequest();
            }

            try {
                categoryService.UpdateCategory(id, categoryDTO);
                return Ok();
            }
            catch (ArgumentNullException e) {
                return BadRequest(e.Message);
            }
            catch (ArgumentException g) {
                return BadRequest(g.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) {
            if (categoryService.RemoveCategory(id) == null) {
                return BadRequest();
            }

            try {
                categoryService.RemoveCategory(id);
                return NoContent();
            }
            catch (ArgumentException e) {
                return NotFound(e.Message);
            }
        }
    }
}
