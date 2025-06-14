using AutoMapper;
using BookStore.Models;
using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<SubmitReviewResponse> AddReview(ReviewRequest request)
        {
            var response = new SubmitReviewResponse(false);

            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null)
            {
                response.Message = "Could not find book";
                return response;
            }

            var existingReview = await _reviewRepository.GetByBookIdAndUsername(request.BookId, request.Username);
            if (existingReview != null)
            {
                response.Message = "You have already left a review for this book";
                return response;
            }
            var newReview = new Review();
            _mapper.Map(request, newReview);
            newReview.Book = book;
            newReview.CreatedDate = DateTime.Now;
            newReview.UpvoteCount = 0;
            newReview.DownvoteCount = 0;
            await _reviewRepository.InsertAsync(newReview);
            response.Message = "Review added";
            response.Success = true;
            response.Id = newReview.Id;

            return response;
        }

        public async Task Upvote(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            review.UpvoteCount++;
            await _reviewRepository.UpdateAsync(review);
        }

        public async Task Downvote(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            review.DownvoteCount++;
            await _reviewRepository.UpdateAsync(review);
        }

        public async Task<ReviewsResponse> GetReviewsForBook(int bookId)
        {
            var reviews = await _reviewRepository.GetByBookId(bookId);
            var dtos = new List<ReviewDto>();
            reviews.ForEach(r =>
            {
                var reviewDto = new ReviewDto();
                _mapper.Map(r, reviewDto);
                dtos.Add(reviewDto);
            });
            return new ReviewsResponse() { Reviews = dtos };
        }
    }
}
