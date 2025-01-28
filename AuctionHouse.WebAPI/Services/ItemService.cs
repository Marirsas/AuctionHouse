using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AutoMapper;
using System.Linq;

namespace AuctionHouse.WebAPI.Services {
    public class ItemService : IItem {
        private readonly AuctionHouseContext context;
        private readonly IMapper mapper;
        public ItemService(IMapper mapper, AuctionHouseContext auctionContext) {
            this.mapper = mapper;
            context = auctionContext;
        }

        public ItemDTO AddItem (ItemDTO item) {
            if (this.context.Items == null) {
                return null;
            }

            if (item.ItemStatus == ItemStatus.Sold) {
                throw new ArgumentException("The Item Status must be available!");
            }

            this.context.Items.Add(mapper.Map<Item>(item));
            this.context.SaveChanges();
            return mapper.Map<ItemDTO>(item);
        }
        public IEnumerable<ItemDTO> GetItems() {
            if (this.context.Items == null) {
                return null;
            }

            var items = context.Items
                           .Select(item => mapper.Map<ItemDTO>(item))
                           .ToList();
            return items;
        }
        public IEnumerable<ItemDTO> GetItemsAvailable() {
            if (this.context.Items == null) {
                return null;
            }

            var items = context.Items
                           .Where(i => i.ItemStatus == ItemStatus.Available)
                           .Select(item => mapper.Map<ItemDTO>(item))
                           .ToList();
            return items;
        }

        public IEnumerable<ItemDTO> GetItemsSold() {
            if (this.context.Items == null) {
                return null;
            }

            var items = context.Items
                           .Where(i => i.ItemStatus == ItemStatus.Sold)
                           .Select(item => mapper.Map<ItemDTO>(item))
                           .ToList();
            return items;
        }

        public ItemDTO GetItem(int id) {
            var item = mapper.Map<ItemDTO>(context.Items.SingleOrDefault(i => i.Id == id));

            if (context.Items == null) {
                return null;
            }

            if (item == null) {
                throw new ArgumentNullException("Item not found!");
            }

            return item;
        }



        public void RemoveItem(int id) {
            throw new NotImplementedException();
        }

        public ItemDTO UpdateItem(int id, Item item) {
            throw new NotImplementedException();
        }
    }
}
