using AutoMapper;
using BookStore.Migrations;
using BookStore.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly UserManager<User> _userManager;
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public OrderService(UserManager<User> userManager,
            IBookRepository bookRepository, 
            IOrderRepository orderRepository, 
            IBookService bookService, 
            IMapper mapper)
        {
            _userManager = userManager;
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<ResponseModel> AddOrder(CompleteOrderRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var order = new Order(user);
            foreach (var orderReq in request.Orders)
            {
                var orderQuantity = new OrderQuantity
                {
                    Quantity = orderReq.OrderCount,
                    Book = await _bookRepository.GetByIdAsync(orderReq.BookId)
                };
                order.OrderQuantities.Add(orderQuantity);
            }

            await _orderRepository.InsertAsync(order);
            await _bookService.UpdateOrderCount(request);
            return new ResponseModel(true);
        }

        public async Task<OrderHistoryResponse> GetOrderHistory(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var responseList = new List<OrderResponse>();
            var orders = await _orderRepository.GetOrdersByUser(user);
            foreach (var order in orders)
            {
                var orderQuantities = new List<OrderQuantityResponse>();
                order.OrderQuantities.ForEach(q =>
                {
                    var orderQuantityToAdd = new OrderQuantityResponse();
                    _mapper.Map(q, orderQuantityToAdd);
                    orderQuantities.Add(orderQuantityToAdd);
                });
                responseList.Add(new OrderResponse()
                {
                    OrderQuantities = orderQuantities,
                    OrderDate = order.OrderDate
                });
            }

            return new OrderHistoryResponse() { Orders = responseList };
        }
    }
}
