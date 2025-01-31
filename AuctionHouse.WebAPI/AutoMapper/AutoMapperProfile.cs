using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AutoMapper;

namespace AuctionHouse.WebAPI.AutoMapper {
    /// <summary>
    /// AutoMapper profile for mapping domain models to DTOs and vice versa.
    /// </summary>
    public class AutoMapperProfile : Profile {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
        /// Configures the mappings between domain models and DTOs.
        /// </summary>
        public AutoMapperProfile() {
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Sale, SaleDTO>().ReverseMap();
        }
    }
}
