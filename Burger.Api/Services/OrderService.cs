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
        

        public OrderItem CalculateCustom(OrderItem item)
        {
            item.Name = "Personalizado";

            foreach (var ingredient in item.Ingredients)
            {
                ingredient.Ingredient = context.Ingredients.Find(ingredient.Ingredient.Id);
                ingredient.Price = ingredient.Ingredient.Price;
            }

            item.Price = item.Ingredients.Sum(i => i.Price * i.Ammount);

            return item;
        }
    }
}