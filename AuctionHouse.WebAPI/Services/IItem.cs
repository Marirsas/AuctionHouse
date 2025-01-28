using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;

namespace AuctionHouse.WebAPI.Services {
    public interface IItem {

        IEnumerable<ItemDTO> GetItems();

        ItemDTO GetItem(int id);

        IEnumerable<ItemDTO> GetItemsAvailable();

        IEnumerable<ItemDTO> GetItemsSold();

        ItemDTO AddItem (ItemDTO item);

        void RemoveItem(int id);

        ItemDTO UpdateItem(int id, Item item);

    }
}
