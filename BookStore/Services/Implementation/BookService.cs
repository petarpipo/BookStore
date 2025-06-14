using AutoMapper;
using BookStore.Models;
using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;

        public BookService(IBookRepository bookRepository, 
            IMapper mapper, IGenreRepository genreRepository, 
            IReviewRepository reviewRepository,
            IAuthorRepository authorRepository, 
            IAuthorService authorService, 
            IGenreService genreService)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _reviewRepository = reviewRepository;
            _authorRepository = authorRepository;
            _authorService = authorService;
            _genreService = genreService;
        }

        public async Task<ResponseModel> SaveBook(NewBookRequest request)
        {
            var book = new Book();
            var response = new ResponseModel(true)
            {
                Message = "Book saved successfully"
            };
            try
            {
                _mapper.Map(request, book);
                book.Authros = await _authorRepository.GetAuthorsByIds(request.AuthorIds);
                book.Genres = await _genreRepository.GetGenresByIds(request.GenreIds);
                await _bookRepository.InsertAsync(book);
            }
            catch
            {
                response.Message = "Error when saving Book";
                response.Success = false;
            }

            return response;
        }


        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdWithInclude(id);
            var bookDto = new BookDto();
            var authors = await _authorService.GetByBookId(book.Id);
            bookDto.Authors = authors.Authors;
            var genres = await _genreService.GetByBookId(book.Id);
            bookDto.Genres = genres;
            if (book.Reviews.Any())
            {
                bookDto.ReviewScore = book.Reviews.Sum(r => r.Score) / (double)book.Reviews.Count();
            }
            _mapper.Map(book, bookDto);
            return bookDto;
        }

        public async Task<BooksResponse> GetBooksByGenreAsync(int id)
        {
            var books = await _bookRepository.GetBooksByGenreId(id);
            var bookDtoList = await BooksToBooksResponse(books);

            return new BooksResponse { Books = bookDtoList };

        }

        public async Task<BooksResponse> GetBooksByIds(int[] ids)
        {
            var books = await _bookRepository.GetBooksByIds(ids);
            var bookDtoList = await BooksToBooksResponse(books);

            return new BooksResponse { Books = bookDtoList };
        }

        public async Task<BooksResponse> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookDtoList = await BooksToBooksResponse(books);

            return new BooksResponse { Books = bookDtoList };
        }

        public async Task<ResponseModel> EditBook(EditBookRequest request)
        {
            var response = new ResponseModel(true)
            {
                Message = "Book saved successfully"
            };
            var book = await _bookRepository.GetBookByIdWithInclude(request.Id);
            if (book == null)
            {
                response.Message = "Can not find book";
                return response;
            }
            try
            {
                _mapper.Map(request, book);
                book.Authros = await _authorRepository.GetAuthorsByIds(request.AuthorIds);
                book.Genres = await _genreRepository.GetGenresByIds(request.GenreIds);
                await _bookRepository.UpdateAsync(book);
            }
            catch
            {
                response.Message = "Error when saving Book";
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseModel> DeleteBook(int bookId)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(bookId);
                await _bookRepository.DeleteAsync(book);
                return new ResponseModel(true) { Message = "Book deleted successfully" };
            }
            catch
            {
                return new ResponseModel(false) { Message = "Something went wrong" };
            }
        }

        public async Task<BooksResponse> GetTopRatedBooks()
        {
            var books = await _bookRepository.GetAllWithReviews();
            books = books.OrderByDescending(b => GetReviewAverage(b.Reviews)).Take(10).ToList();
            var bookDtoList = await BooksToBooksResponse(books);

            return new BooksResponse { Books = bookDtoList };
        }

        public async Task<BooksResponse> GetBestSellerBooks()
        {
            var books = await _bookRepository.GetBestSellerBooks();
            var bookDtoList = await BooksToBooksResponse(books);

            return new BooksResponse { Books = bookDtoList };
        }

        public async Task<BooksResponse> GetByAuthorId(int id)
        {
            var books = await _bookRepository.GetByAuthorId(id);
            var bookDtoList = await BooksToBooksResponse(books);

            return new BooksResponse { Books = bookDtoList };
        }
        private async Task<List<BookDto>> BooksToBooksResponse(IEnumerable<Book> books)
        {
            var bookDtoList = new List<BookDto>();
            foreach (var book in books)
            {
                var bookDto = new BookDto();
                _mapper.Map(book, bookDto);
                var authors = await _authorService.GetByBookId(book.Id);
                bookDto.Authors = authors.Authors;
                var genres = await _genreService.GetByBookId(book.Id);
                var reviews = await _reviewRepository.GetByBookId(book.Id);
                if (reviews.Count > 0)
                    bookDto.ReviewScore = reviews.Sum(r => r.Score) / (double)reviews.Count();
                bookDto.Genres = genres;

                bookDtoList.Add(bookDto);
            }

            return bookDtoList;
        }


        private float GetReviewAverage(List<Review> reviews)
        {
            if (reviews == null || !reviews.Any())
                return 0;
            return reviews.Sum(r => r.Score) / (float)reviews.Count();
        }

        public async Task<ResponseModel> UpdateOrderCount(CompleteOrderRequest request)
        {
            var books = await _bookRepository.GetBooksByIds(request.Orders.Select(d => d.BookId).ToArray());
            books.ForEach(b =>
            {
                b.OrderCount += request.Orders.FirstOrDefault(o => o.BookId == b.Id).OrderCount;
            });
            await _bookRepository.UpdateMultipleAsync(books);
            return new ResponseModel(true);
        }
    }
}
