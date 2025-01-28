using AuctionHouse.WebAPI.Models;

namespace AuctionHouse.WebAPI.DTO {
    public class CategoryDTO {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ItemDTO>? Items { get; set; }

        public CategoryDTO(int id, string name, string description, ICollection<Item> items)
        {
            Id = id;
            Name = name;
            Description = description;
            Items = itemsToDTO(items);
        }

        private ICollection<ItemDTO> itemsToDTO (ICollection<Item> items) {

            ICollection<ItemDTO> itemsDTO = new List<ItemDTO>();

            if (items != null) {
                foreach (var item in items) {
                    itemsDTO.Add(ItemDTO.itemToDTO(item));
                }
            }

            return itemsDTO;
        }


    }
}
