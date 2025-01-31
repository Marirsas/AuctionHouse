using AuctionHouse.WebAPI.Data;
using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AuctionHouse.WebAPI.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.WebAPI.Services {
    public class SalesService : ISalesService {

        private readonly AuctionHouseContext context;
        private readonly IMapper mapper;
        private readonly IItemsService itemsService;

        public SalesService(IMapper mapper, AuctionHouseContext auctionContext, IItemsService itemsService) {
            this.mapper = mapper;
            context = auctionContext;
            this.itemsService = itemsService;
        }


        public IEnumerable<SaleDTO> GetSales() {
            if (this.context.Sales == null) {
                return null;
            }

            return context.Sales.Select(Sales => mapper.Map<SaleDTO>(Sales)).ToList();
        }

        public IEnumerable<SaleDTO> GetSalesByItemCategory(int categoryId) {
            if (this.context.Sales == null) {
                return null;
            }

            return context.Sales
                .Where(sale => sale.Item != null && sale.Item.CategoryId == categoryId)
                .Select(sale => mapper.Map<SaleDTO>(sale))
                .ToList();
        }

        public SaleDTO GetSale(int id) {
            if (this.context.Sales == null) {
                return null;
            }

            var sale = context.Sales.SingleOrDefault(s => s.Id == id);
            if (sale == null) {
                throw new ArgumentNullException("Sale not found!");
            }
            return mapper.Map<SaleDTO>(sale);
        }

        public SaleDTO GetSaleByItemId(int itemId) {
            if (this.context.Sales == null) {
                return null;
            }

            var sale = context.Sales.SingleOrDefault(s => s.ItemId == itemId);
            if (sale == null) {
                throw new ArgumentNullException("Sale not found!");
            }
            return mapper.Map<SaleDTO>(sale);
        }

        public SaleDTO AddSale(SaleDTO saleDTO) {
            var item = context.Items.SingleOrDefault(i => i.Id == saleDTO.ItemId);
            if (item == null) {
                throw new ArgumentNullException("Provided itemId doesn't exists!");
            }

            if(saleDTO.SalePrice < item.InitialPrice) {
                throw new ArgumentException("Sale price can't be lower than item initial price!");
            }

            if(item.ItemStatus == ItemStatus.Sold) {
                throw new InvalidOperationException("Failed to add new sale. The item is already sold!");
            }

            var sale = new Sale {
                ItemId = saleDTO.ItemId,
                Item = item,
                DateOfSale = DateOnly.FromDateTime(DateTime.Now),
                SalePrice = saleDTO.SalePrice
            };

            itemsService.UpdateItemStatus(saleDTO.ItemId);
            context.Sales.Add(sale);
            context.SaveChanges();

            return mapper.Map<SaleDTO>(sale);
        }

        public int GetSalesQuantity() {
            if (this.context.Sales == null) {
                return 0;
            }
            return context.Sales.Count();
        }

        public int GetSalesQuantityByItemCategory(int categoryId) {
            if (context.Sales == null) {
                return 0;
            }
            return context.Sales.Count(sale => sale.Item != null && sale.Item.CategoryId == categoryId);
        }

        public decimal GetTotalSalesPrice() {
            if (context.Sales == null) {
                return 0;
            }
            return context.Sales.Sum(sale => sale.SalePrice);
        }

        public decimal GetTotalSalesPriceByItemCategory(int categoryId) {
            if (context.Sales == null) {
                return 0;
            }
            return context.Sales
                .Where(sale => sale.Item != null && sale.Item.CategoryId == categoryId)
                .Sum(sale => sale.SalePrice);
        }
    }
}
