using AutoMapper;
using BookStore.Models;
using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.MapperProfiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<NewAuthorRequest,Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<EditAuthorRequest, Author>();
            CreateMap<Author, SearchDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => "author"));
        }
    }
}
