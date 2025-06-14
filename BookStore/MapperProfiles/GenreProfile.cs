using AutoMapper;
using BookStore.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.MapperProfiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<NewGenreRequest, Genre>();
            CreateMap<Genre, GenreResponse>();
            CreateMap<Genre, SearchDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s =>"genre"));
        }
    }
}
