namespace BookStore.Models.Responses
{
    public class SubmitReviewResponse : ResponseModel
    {
        public int Id { get; set; }
        public SubmitReviewResponse(bool success) : base(success)
        {
        }
    }
}
