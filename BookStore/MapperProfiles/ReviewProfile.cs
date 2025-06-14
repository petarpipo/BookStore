using AutoMapper;
using BookStore.Models;
using BookStore.Models.Dto;
using BookStore.Models.Requests;

namespace BookStore.MapperProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewRequest, Review>();
            CreateMap<Review,ReviewDto>();
        }
    }
}
