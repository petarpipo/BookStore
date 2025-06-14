using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] CompleteOrderRequest request)
        {
            return Ok(await _orderService.AddOrder(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderHistory(string username)
        {
            return Ok(await _orderService.GetOrderHistory(username));
        }
    }
}
