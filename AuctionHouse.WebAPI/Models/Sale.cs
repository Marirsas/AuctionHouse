namespace AuctionHouse.WebAPI.Models {
    public class Sale {

        public int Id { get; set; }


        public int ItemId { get; set; }


        public DateOnly DateOfSale { get; set; }


        public decimal SalePrice { get; set; }


    }
}
