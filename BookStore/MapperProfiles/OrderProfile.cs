using AutoMapper;
using BookStore.Models;
using BookStore.Models.Responses;

namespace BookStore.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderQuantity, OrderQuantityResponse>();
        }
    }
}
