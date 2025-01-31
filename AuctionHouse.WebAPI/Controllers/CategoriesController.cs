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

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of CategoryDTO objects.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories() {

            if (categoryService.GetCategories() == null) {
                return BadRequest();
            }

            return Ok(categoryService.GetCategories());

        }

        /// <summary>
        /// Retrieves a specific category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A CategoryDTO object.</returns>
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

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="categoryDTO">The category data to add.</param>
        /// <returns>The created CategoryDTO object.</returns>
        [HttpPost]
        public ActionResult<CategoryDTO> AddCategory(CategoryDTO categoryDTO) {

            if (categoryDTO == null) {
                return BadRequest();
            }

            var newCategory = categoryService.AddCategory(categoryDTO);

            return CreatedAtAction(nameof(GetCategory), new { id = newCategory }, newCategory);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDTO">The updated category data.</param>
        /// <returns>The updated CategoryDTO object.</returns>
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

        /// <summary>
        /// Removes a category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to remove.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public IActionResult RemoveCategory(int id) {
            try {
                if (categoryService.RemoveCategory(id) == null) {
                    return BadRequest();
                }

                categoryService.RemoveCategory(id);
                return NoContent();

            }
            catch (ArgumentException e) {
                return NotFound(e.Message);
            }
        }
    }
}
