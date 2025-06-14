using System.Net;

namespace BookStore.Models.Responses
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ResponseModel(bool success)
        {
            Success = success;
        }
    }
}
