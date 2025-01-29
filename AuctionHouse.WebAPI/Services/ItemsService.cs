using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AuctionHouse.WebAPI.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AuctionHouse.WebAPI.Services
{
    public class ItemsService : IItemsService {
        private readonly AuctionHouseContext context;
        private readonly IMapper mapper;

        public ItemsService(IMapper mapper, AuctionHouseContext auctionContext) {
            this.mapper = mapper;
            context = auctionContext;
        }


        public IEnumerable<ItemDTO> GetItems() {

            if (this.context.Items == null) {
                return null;
            }
                           
            return context.Items.Select(item => mapper.Map<ItemDTO>(item)).ToList();
        }

        public IEnumerable<ItemDTO> GetItemsAvailable() {

            if (this.context.Items == null) {
                return null;
            }

            return context.Items
                          .Where(i => i.ItemStatus == ItemStatus.Available)
                          .Select(item => mapper.Map<ItemDTO>(item))
                          .ToList();           
        }

        public IEnumerable<ItemDTO> GetItemsSold() {

            if (this.context.Items == null) {
                return null;
            }

            return context.Items
                          .Where(i => i.ItemStatus == ItemStatus.Sold)
                          .Select(item => mapper.Map<ItemDTO>(item))
                          .ToList();
        }

        public IEnumerable<ItemDTO> GetItemsByCategory(int id) {

            if (this.context.Items == null) {
                return null;
            }

            return context.Items
                          .Where(i => i.CategoryId == id)
                          .Select(item => mapper.Map<ItemDTO>(item))
                          .ToList();
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

        public ItemDTO AddItem(ItemDTO itemDTO) {

            if (itemDTO.ItemStatus == ItemStatus.Sold) {
                throw new InvalidOperationException("Unable to add an item with sold status!");
            }

            if(itemDTO.InitialPrice < 1) {
                throw new ArgumentOutOfRangeException("The price must be higher than 0");
            }

            var category = context.Categories.SingleOrDefault(c => c.Id == itemDTO.CategoryId);
            if (category == null) {
                throw new ArgumentException("Category doesn't exists!");
            }

            var itemTemp = new Item {
                Name = itemDTO.Name,
                Description = itemDTO.Description,
                InitialPrice = itemDTO.InitialPrice,
                ItemStatus = itemDTO.ItemStatus,
                CategoryId = itemDTO.CategoryId,
                Category = category
            };
            this.context.Items.Add(itemTemp);
            this.context.SaveChanges();

            return mapper.Map<ItemDTO>(itemTemp);
        }


        public ItemDTO RemoveItem(int id) {

            if (this.context.Items == null) {
                return null;
            }

            var item = context.Items.SingleOrDefault(i => i.Id == id);
            if (item == null) {
                return null;
            }

            this.context.Items.Remove(item);
            this.context.SaveChanges();

            return mapper.Map<ItemDTO>(item);
        }

        public ItemDTO UpdateItem(int id, ItemDTO itemDTO) {

            if (this.context.Items == null) {
                return null;
            }
            
            if (id != itemDTO.Id) {
                throw new ArgumentException("The ID provided doesn't match the ID of the item to be updated");
            }

            var itemToCheckStatus = context.Items.SingleOrDefault(i => i.Id == id);
            if (itemToCheckStatus.ItemStatus == ItemStatus.Sold || itemToCheckStatus == null) {
                throw new InvalidOperationException("Items with sold status can't be updated");
            }

            var category = context.Categories.SingleOrDefault(c => c.Id == itemDTO.CategoryId);
            if (category == null) {
                throw new ArgumentNullException("Category doesn't exists!");
            }
            mapper.Map(itemDTO, itemToCheckStatus);
            this.context.Entry(itemToCheckStatus).State = EntityState.Modified;
            this.context.SaveChanges();

            return mapper.Map<ItemDTO>(itemToCheckStatus);
        }

        public bool UpdateItemStatus(int itemId) {
            var item = context.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null) {
                throw new ArgumentNullException("Item not found!");
            }

            if(item.ItemStatus == ItemStatus.Sold) {
                throw new InvalidOperationException("Item already sold");
            }

            item.ItemStatus = ItemStatus.Sold;
            context.SaveChanges();

            return true; 
        }
    }
}
