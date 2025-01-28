using AuctionHouse.WebAPI.DTO;

namespace AuctionHouse.WebAPI.Services.Interfaces {
    public interface ISalesService {

        IEnumerable<SaleDTO> GetSales();

        IEnumerable<SaleDTO> GetSalesByItemCategory(int categoryId);

        SaleDTO GetSale(int id);

        SaleDTO GetSaleByItemId(int itemId);

        SaleDTO AddSale(int itemId, DateOnly dateOfSale, decimal salePrice);

        decimal GetTotalSalesPrice();

        decimal GetTotalSalesPriceByItemCategory(int categoryId);

        int GetSalesQuantity();

        int GetSalesQuantityByItemCategory(int categoryId);
    }
}
