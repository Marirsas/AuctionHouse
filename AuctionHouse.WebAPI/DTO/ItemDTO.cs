using AuctionHouse.WebAPI.Models;

namespace AuctionHouse.WebAPI.DTO {
    public class ItemDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal InitialPrice { get; set; }
        public int CategoryId { get; set; }
        public ItemStatus ItemStatus { get; set; }

        public ItemDTO(int id, string name, string description, decimal initialPrice, int categoryId, ItemStatus itemStatus) {
            Id = id;
            Name = name;
            Description = description;
            InitialPrice = initialPrice;
            CategoryId = categoryId;
            ItemStatus = itemStatus;
        }

    }

}

