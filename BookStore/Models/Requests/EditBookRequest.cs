namespace BookStore.Models.Requests
{
    public class EditBookRequest : NewBookRequest
    {
        public int Id { get; set; }
    }
}
