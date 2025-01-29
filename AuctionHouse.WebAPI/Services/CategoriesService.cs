using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AuctionHouse.WebAPI.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.WebAPI.Services
{
    public class CategoriesService : ICategoriesService {
        private readonly AuctionHouseContext context;
        private readonly IMapper mapper;
        public CategoriesService(IMapper mapper, AuctionHouseContext auctionContext) {
            this.mapper = mapper;
            context = auctionContext;
        }
        public IEnumerable<CategoryDTO> GetCategories() {

            if (this.context.Categories == null) {
                return null;
            }

            return context.Categories
                           .Select(category => mapper.Map<CategoryDTO>(category))
                           .ToList();
        }

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

        public CategoryDTO AddCategory(CategoryDTO categoryDTO) {
            if (this.context.Categories == null) {
                return null;
            }

            var newCategory = mapper.Map<Category>(categoryDTO);
            this.context.Categories.Add(newCategory);
            this.context.SaveChanges();

            return mapper.Map<CategoryDTO>(newCategory);
        }

        public CategoryDTO RemoveCategory(int id) {
            if (this.context.Categories == null) {
                return null;
            }

            var category = this.context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null) {
               return null;
            }

            this.context.Categories.Remove(category);
            this.context.SaveChanges();

            return mapper.Map<CategoryDTO>(category);
        }

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
