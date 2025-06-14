using AutoMapper;
using BookStore.Models;
using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.MapperProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<NewBookRequest, Book>()
                .ForMember(o => o.Authros, s => s.Ignore())
                .ForMember(o => o.Reviews, s => s.Ignore());

            CreateMap<Book,BookDto>()
                .ForMember(o => o.ReleaseDate, s => s.MapFrom(o => o.ReleaseDate.ToShortDateString()));

            CreateMap<Book, SearchDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => "book"));
            CreateMap<Book, OrderBookResponse>()
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price.ToString()));
        }
    }
}
