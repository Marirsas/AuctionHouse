using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AuctionHouse.WebAPI.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.WebAPI.Services
{
    /// <summary>
    /// Service for managing categories in the auction house.
    /// </summary>
    public class CategoriesService : ICategoriesService {
        private readonly AuctionHouseContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesService"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="auctionContext">The database context for the auction house.</param>
        public CategoriesService(IMapper mapper, AuctionHouseContext auctionContext) {
            this.mapper = mapper;
            context = auctionContext;
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A collection of CategoryDTO objects.</returns>
        public IEnumerable<CategoryDTO> GetCategories() {
            if (this.context.Categories == null) {
                return null;
            }

            return context.Categories
                           .Select(category => mapper.Map<CategoryDTO>(category))
                           .ToList();
        }

        /// <summary>
        /// Gets a category by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the category.</param>
        /// <returns>A CategoryDTO object.</returns>
        public CategoryDTO GetCategory(int id) {
            var category = mapper.Map<CategoryDTO>(context.Categories.SingleOrDefault(c => c.Id == id));

            if (context.Categories == null) {
                return null;
            }

            if (category == null) {
                throw new ArgumentNullException("Category not found!");
            }

            return category;
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="categoryDTO">The category data transfer object.</param>
        /// <returns>The added CategoryDTO object.</returns>
        public CategoryDTO AddCategory(CategoryDTO categoryDTO) {
            if (this.context.Categories == null) {
                return null;
            }

            var newCategory = mapper.Map<Category>(categoryDTO);
            this.context.Categories.Add(newCategory);
            this.context.SaveChanges();

            return mapper.Map<CategoryDTO>(newCategory);
        }

        /// <summary>
        /// Removes a category by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the category to remove.</param>
        /// <returns>The removed CategoryDTO object.</returns>
        public CategoryDTO RemoveCategory(int id) {
            if (this.context.Categories == null) {
                return null;
            }

            var category = this.context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null) {
                throw new ArgumentException("The category doesn't exists!");
            }

            this.context.Categories.Remove(category);
            this.context.SaveChanges();

            return mapper.Map<CategoryDTO>(category);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The identifier of the category to update.</param>
        /// <param name="categoryDTO">The updated category data transfer object.</param>
        /// <returns>The updated CategoryDTO object.</returns>
        public CategoryDTO UpdateCategory(int id, CategoryDTO categoryDTO) {
            if (this.context.Categories == null) {
                return null;
            }

            if (id != categoryDTO.Id) {
                throw new ArgumentException("The ID provided doesn't match the ID of the category to be updated");
            }

            var existingCategory = context.Categories.Find(id);
            if (existingCategory == null) {
                throw new ArgumentNullException("Category doesn't exist!");
            }

            mapper.Map(categoryDTO, existingCategory);
            context.Entry(existingCategory).State = EntityState.Modified;
            this.context.SaveChanges();

            return mapper.Map<CategoryDTO>(existingCategory);
        }
    }
}
