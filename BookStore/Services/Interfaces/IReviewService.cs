using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.Services.Interfaces
{
    public interface IReviewService
    {
        Task Downvote(int id);
        Task Upvote(int id);
        Task<SubmitReviewResponse> AddReview(ReviewRequest request);
        Task<ReviewsResponse> GetReviewsForBook(int bookId);
    }
}
