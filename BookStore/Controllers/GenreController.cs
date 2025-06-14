using BookStore.Models.Requests;
using BookStore.Models.Responses;
using BookStore.Services.Implementation;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveGenre([FromBody] NewGenreRequest genreRequest)
        {
            return Ok(await _genreService.SaveGenre(genreRequest));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            return Ok(await _genreService.GetAllGenres());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            return Ok(await _genreService.GetDtoById(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditGenre([FromBody] EditGenreRequest request)
        {
            return Ok(await _genreService.EditGenre(request));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            return Ok(await _genreService.DeleteGenre(id));
        }
    }
}
