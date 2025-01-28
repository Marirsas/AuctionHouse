using AuctionHouse.WebAPI.DTO;
using AuctionHouse.WebAPI.Models;
using AutoMapper;

namespace AuctionHouse.WebAPI.AutoMapper {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
