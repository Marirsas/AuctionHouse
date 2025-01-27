using System.ComponentModel.DataAnnotations;

namespace AuctionHouse.WebAPI.Models {
    public class Category {


        [Key]
        public int Id { get; set; }


        [MaxLength(100)]
        public string? Name { get; set; }


        [MaxLength (150)]
        public string? Description { get; set; }


        public ICollection<Item>? Items { get; set; }

    }
}
