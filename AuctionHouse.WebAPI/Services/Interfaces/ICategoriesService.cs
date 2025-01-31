using AuctionHouse.WebAPI.DTO;

namespace AuctionHouse.WebAPI.Services.Interfaces
{
    /// <summary>
    /// Interface for category service operations.
    /// </summary>
    public interface ICategoriesService {
        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A collection of CategoryDTO objects.</returns>
        IEnumerable<CategoryDTO> GetCategories();

        /// <summary>
        /// Gets a category by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the category.</param>
        /// <returns>A CategoryDTO object.</returns>
        CategoryDTO GetCategory(int id);

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="categoryDTO">The category data transfer object.</param>
        /// <returns>The added CategoryDTO object.</returns>
        CategoryDTO AddCategory(CategoryDTO categoryDTO);

        /// <summary>
        /// Removes a category by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the category to remove.</param>
        /// <returns>The removed CategoryDTO object.</returns>
        CategoryDTO RemoveCategory(int id);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The identifier of the category to update.</param>
        /// <param name="categoryDTO">The updated category data transfer object.</param>
        /// <returns>The updated CategoryDTO object.</returns>
        CategoryDTO UpdateCategory(int id, CategoryDTO categoryDTO);
    }
}
