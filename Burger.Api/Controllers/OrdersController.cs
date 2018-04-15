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


        // POST orders/calculate-custom
        [Route("calculate-custom")]
        [HttpPost]
        public OrderItem Post([FromBody]OrderItem item)
        {
            return orderService.CalculateCustom(item);
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