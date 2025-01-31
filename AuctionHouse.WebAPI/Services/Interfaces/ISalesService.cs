using AuctionHouse.WebAPI.DTO;

namespace AuctionHouse.WebAPI.Services.Interfaces {
    /// <summary>
    /// Interface for sales service operations.
    /// </summary>
    public interface ISalesService {

        /// <summary>
        /// Gets all sales.
        /// </summary>
        /// <returns>A collection of SaleDTO objects.</returns>
        IEnumerable<SaleDTO> GetSales();

        /// <summary>
        /// Gets sales by item category.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns>A collection of SaleDTO objects.</returns>
        IEnumerable<SaleDTO> GetSalesByItemCategory(int categoryId);

        /// <summary>
        /// Gets a sale by ID.
        /// </summary>
        /// <param name="id">The sale ID.</param>
        /// <returns>A SaleDTO object.</returns>
        SaleDTO GetSale(int id);

        /// <summary>
        /// Gets a sale by item ID.
        /// </summary>
        /// <param name="itemId">The item ID.</param>
        /// <returns>A SaleDTO object.</returns>
        SaleDTO GetSaleByItemId(int itemId);

        /// <summary>
        /// Adds a new sale.
        /// </summary>
        /// <param name="saleDTO">The sale data transfer object.</param>
        /// <returns>The added SaleDTO object.</returns>
        SaleDTO AddSale(SaleDTO saleDTO);

        /// <summary>
        /// Gets the total sales price.
        /// </summary>
        /// <returns>The total sales price.</returns>
        decimal GetTotalSalesPrice();

        /// <summary>
        /// Gets the total sales price by item category.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns>The total sales price for the specified category.</returns>
        decimal GetTotalSalesPriceByItemCategory(int categoryId);

        /// <summary>
        /// Gets the total sales quantity.
        /// </summary>
        /// <returns>The total sales quantity.</returns>
        int GetSalesQuantity();

        /// <summary>
        /// Gets the total sales quantity by item category.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns>The total sales quantity for the specified category.</returns>
        int GetSalesQuantityByItemCategory(int categoryId);
    }
}
