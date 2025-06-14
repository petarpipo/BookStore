using BookStore.Models.Dto;

namespace BookStore.Models.Requests
{
    public class CompleteOrderRequest
    {
        public List<OrderDto> Orders { get; set; }
        public string UserName { get; set; }
    }
}
