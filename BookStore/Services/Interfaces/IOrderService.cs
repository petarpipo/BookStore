using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseModel> AddOrder(CompleteOrderRequest request);
        Task<OrderHistoryResponse> GetOrderHistory(string username);
    }
}
