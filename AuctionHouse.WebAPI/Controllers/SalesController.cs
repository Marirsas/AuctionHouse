using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase {
        private readonly ISalesService salesService;

        public SalesController(ISalesService salesService) {
            this.salesService = salesService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<SaleDTO>> GetSales() {
            var sales = salesService.GetSales();
            if (sales == null || !sales.Any()) {
                return NotFound("No sales found.");
            }
            return Ok(sales);
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<SaleDTO>> GetSalesByItemCategory(int categoryId) {
            var sales = salesService.GetSalesByItemCategory(categoryId);
            if (sales == null || !sales.Any()) {
                return NotFound($"No sales found for category ID {categoryId}.");
            }
            return Ok(sales);
        }

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

        [HttpGet("quantity")]
        public ActionResult<int> GetSalesQuantity() {
            int quantity = salesService.GetSalesQuantity();
            return Ok(quantity);
        }

        [HttpGet("quantity/category/{categoryId}")]
        public ActionResult<int> GetSalesQuantityByItemCategory(int categoryId) {
            int quantity = salesService.GetSalesQuantityByItemCategory(categoryId);
            return Ok(quantity);
        }

        [HttpGet("total-price")]
        public ActionResult<decimal> GetTotalSalesPrice() {
            decimal totalPrice = salesService.GetTotalSalesPrice();
            return Ok(totalPrice);
        }

        [HttpGet("total-price/category/{categoryId}")]
        public ActionResult<decimal> GetTotalSalesPriceByItemCategory(int categoryId) {
            decimal totalPrice = salesService.GetTotalSalesPriceByItemCategory(categoryId);
            return Ok(totalPrice);
        }

        [HttpPost]
        public ActionResult<SaleDTO> AddSale(int itemId, DateOnly dateOfSale, decimal salePrice) {
            try {
                var sale = salesService.AddSale(itemId, dateOfSale,salePrice);
                return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
            }
            catch (ArgumentNullException ex) {
                return BadRequest(ex.Message);
            }
        }


    }
}
