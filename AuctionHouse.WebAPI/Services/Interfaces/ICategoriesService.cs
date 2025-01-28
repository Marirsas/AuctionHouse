using AuctionHouse.WebAPI.DTO;

namespace AuctionHouse.WebAPI.Services.Interfaces
{
    public interface ICategoriesService
    {

        IEnumerable<CategoryDTO> GetCategories();

        CategoryDTO GetCategory(int id);

        CategoryDTO AddCategory(CategoryDTO categoryDTO);

        CategoryDTO RemoveCategory(int id);

        CategoryDTO UpdateCategory(int id, CategoryDTO categoryDTO);
    }
}
