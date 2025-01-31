using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase {
        private readonly ISalesService salesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesController"/> class.
        /// </summary>
        /// <param name="salesService">The sales service.</param>
        public SalesController(ISalesService salesService) {
            this.salesService = salesService;
        }

        /// <summary>
        /// Gets all sales.
        /// </summary>
        /// <returns>A list of sales.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<SaleDTO>> GetSales() {
            var sales = salesService.GetSales();
            if (sales == null || !sales.Any()) {
                return NotFound("No sales found.");
            }
            return Ok(sales);
        }

        /// <summary>
        /// Gets sales by item category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>A list of sales for the specified category.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<SaleDTO>> GetSalesByItemCategory(int categoryId) {
            var sales = salesService.GetSalesByItemCategory(categoryId);
            if (sales == null || !sales.Any()) {
                return NotFound($"No sales found for category ID {categoryId}.");
            }
            return Ok(sales);
        }

        /// <summary>
        /// Gets a sale by identifier.
        /// </summary>
        /// <param name="id">The sale identifier.</param>
        /// <returns>The sale with the specified identifier.</returns>
        [HttpGet("{id}")]
        public ActionResult<SaleDTO> GetSale(int id) {
            try {
                var sale = salesService.GetSale(id);
                return Ok(sale);
            }
            catch (ArgumentNullException ex) {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets a sale by item identifier.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>The sale with the specified item identifier.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("item/{itemId}")]
        public ActionResult<SaleDTO> GetSaleByItemId(int itemId) {
            try {
                var sale = salesService.GetSaleByItemId(itemId);
                return Ok(sale);
            }
            catch (ArgumentNullException ex) {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets the total quantity of sales.
        /// </summary>
        /// <returns>The total quantity of sales.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("quantity")]
        public ActionResult<int> GetSalesQuantity() {
            int quantity = salesService.GetSalesQuantity();
            return Ok(quantity);
        }

        /// <summary>
        /// Gets the total quantity of sales by item category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>The total quantity of sales for the specified category.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("quantity/category/{categoryId}")]
        public ActionResult<int> GetSalesQuantityByItemCategory(int categoryId) {
            int quantity = salesService.GetSalesQuantityByItemCategory(categoryId);
            return Ok(quantity);
        }

        /// <summary>
        /// Gets the total sales price.
        /// </summary>
        /// <returns>The total sales price.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("total-price")]
        public ActionResult<decimal> GetTotalSalesPrice() {
            decimal totalPrice = salesService.GetTotalSalesPrice();
            return Ok(totalPrice);
        }

        /// <summary>
        /// Gets the total sales price by item category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>The total sales price for the specified category.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("total-price/category/{categoryId}")]
        public ActionResult<decimal> GetTotalSalesPriceByItemCategory(int categoryId) {
            decimal totalPrice = salesService.GetTotalSalesPriceByItemCategory(categoryId);
            return Ok(totalPrice);
        }

        /// <summary>
        /// Adds a new sale.
        /// </summary>
        /// <param name="saleDTO">The sale data transfer object.</param>
        /// <returns>The created sale.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<SaleDTO> AddSale(SaleDTO saleDTO) {
            try {
                var sale = salesService.AddSale(saleDTO);
                return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
            }
            catch (ArgumentNullException ex) {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException f) {
                return BadRequest(f.Message);
            }
        }
    }
}
