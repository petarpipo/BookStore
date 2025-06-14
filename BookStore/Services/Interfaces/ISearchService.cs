using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.Services.Interfaces
{
    public interface ISearchService
    {
        Task<SearchResponse> Search(SearchRequest request);
    }
}
