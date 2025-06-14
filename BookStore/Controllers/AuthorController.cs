using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAuthor([FromBody] NewAuthorRequest request)
        {
            return Ok(await _authorService.SaveAuthor(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _authorService.GetAllAuthors());
        }

        [Route("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            return Ok(await _authorService.GetDtoById(id));
        }

        public async Task<IActionResult> EditAuthor([FromBody] EditAuthorRequest request)
        {
            return Ok(await _authorService.EditAuthor(request));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return Ok(await _authorService.DeleteAuthor(id));
        }
    }
}
