using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Burger.Api.Data;
using Burger.Api.Models;

namespace Burger.Api.Services
{
    public class OrderService
    {
        private readonly Context context;

        public OrderService(Context context)
        {
            this.context = context;
        }
        

        public IEnumerable<Order> GetLatest()
        {
            return context.Orders
                .Include("Items.Ingredients.Ingredient")
                .OrderByDescending(o => o.EntryDate);
        }

        public OrderItem CalculateCustom(OrderItem item)
        {
            item.Name = item.Name ?? "Personalizado";

            foreach (var ingredient in item.Ingredients)
            {
                ingredient.Ingredient = context.Ingredients.Find(ingredient.Ingredient.Id);
                ingredient.Price = ingredient.Ingredient.Price;
            }

            item.Price = item.Ingredients.Sum(i => i.Price * i.Ammount);

            return item;
        }

        public Order Save(Order order)
        {
            foreach (var item in order.Items)
            {
                item.Order = order;

                foreach (var ingredient in item.Ingredients)
                {
                    ingredient.OrderItem = item;
                    ingredient.Ingredient = context.Ingredients.Find(ingredient.Ingredient.Id);
                }
            }

            context.Orders.Add(order);

            context.SaveChanges();

            return order;
        }
    }
}