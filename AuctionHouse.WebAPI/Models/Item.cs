using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionHouse.WebAPI.Models {
    public class Item {


        [Key]
        public int Id { get; set; }


        [MaxLength(100)]
        public string? Name { get; set; }


        [MaxLength(150)]
        public string? Description { get; set; }


        [Column(TypeName = "decimal(6, 2)")]
        public decimal InitialPrice {  get; set; }


        public Category? Category { get; set; }


        public int CategoryId { get; set; }


        public ItemStatus ItemStatus { get; set; }



    }
}
