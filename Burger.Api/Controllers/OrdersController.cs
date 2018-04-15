using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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



        //// GET orders
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET orders/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST orders
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT orders/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE orders/5
        //public void Delete(int id)
        //{
        //}
    }
}