using BookStore.Models.Requests;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] SearchRequest request)
        {
            return Ok(await _searchService.Search(request));
        }
    }
}
