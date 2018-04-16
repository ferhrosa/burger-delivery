using System.Collections.Generic;
using System.Web.Http;
using Burger.Api.Models;
using Burger.Api.Services;

namespace Burger.Api.Controllers
{
    [RoutePrefix("orders")]
    public class OrdersController : ApiController
    {
        private readonly OrderService orderService;

        public OrdersController(OrderService orderService)
        {
            this.orderService = orderService;
        }


        // GET latest
        [Route("latest")]
        [HttpGet]
        public IEnumerable<Order> GetLatest()
        {
            return orderService.GetLatest();
        }

        // POST orders/calculate-custom
        [Route("calculate-custom")]
        [HttpPost]
        public OrderItem Post([FromBody]OrderItem item)
        {
            return orderService.CalculateItem(item);
        }

        // POST orders
        [Route("")]
        [HttpPost]
        public Order Save([FromBody]Order order)
        {
            return orderService.Save(order);
        }

    }
}