using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;

namespace AuctionHouse.WebAPI.Services.Interfaces
{
    public interface IItemsService
    {

        IEnumerable<ItemDTO> GetItems();

        ItemDTO GetItem(int id);

        IEnumerable<ItemDTO> GetItemsAvailable();

        IEnumerable<ItemDTO> GetItemsSold();

        IEnumerable<ItemDTO> GetItemsByCategory(int id);

        ItemDTO AddItem(ItemDTO item);

        ItemDTO RemoveItem(int id);

        ItemDTO UpdateItem(int id, ItemDTO itemDTO);

        Boolean UpdateItemStatus(int itemId);
    }
}
