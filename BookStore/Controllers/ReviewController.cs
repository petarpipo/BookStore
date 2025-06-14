using BookStore.Models.Requests;
using BookStore.Services.Implementation;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview([FromBody] ReviewRequest request)
        {
            return Ok(await _reviewService.AddReview(request));
        }
        [HttpPost]
        public async Task<IActionResult> Upvote([FromBody] UpDownVoteRequest request)
        {
            await _reviewService.Upvote(request.Id);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Downvote([FromBody] UpDownVoteRequest request)
        {
            await _reviewService.Downvote(request.Id);
            return Ok();
        }

        [Route("{Id}")]
        public async Task<IActionResult> GetReviews(int id)
        {
            return Ok(await _reviewService.GetReviewsForBook(id));
        }
    }
}
