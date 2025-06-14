using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var response = await _bookService.GetAllBooksAsync();
            return Ok(response);
        }
        [Route("{Id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _bookService.GetBookByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> SaveBook([FromBody]NewBookRequest request)
        {
            return Ok(await _bookService.SaveBook(request));
        }

        public async Task<IActionResult> GetBooksByIds([FromQuery] int[] id)
        {
            return Ok(await _bookService.GetBooksByIds(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditBook([FromBody] EditBookRequest request)
        {
            return Ok(await _bookService.EditBook(request));
        }

        [Route("{Id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _bookService.DeleteBook(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetTopRatedBooks()
        {
            var response = await _bookService.GetTopRatedBooks();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetBestSellerBooks()
        {
            var response = await _bookService.GetBestSellerBooks();
            return Ok(response);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetBooksByAuthorId(int id)
        {
            return Ok(await _bookService.GetByAuthorId(id));
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetBooksByGenreId(int id)
        {
            return Ok(await _bookService.GetBooksByGenreAsync(id));
        }
    } 
}
