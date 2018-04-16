using System;
using System.Collections.Generic;
using System.Linq;
using Burger.Api.Data;
using Burger.Api.Models;

namespace Burger.Api.Services
{
    public class OrderService
    {
        public const int IngredientSaleAmmount = 3;

        private readonly Context context;

        public OrderService(Context context = null)
        {
            this.context = context;
        }
        

        public IEnumerable<Order> GetLatest()
        {
            return context.Orders
                .Include("Items.Ingredients.Ingredient")
                .OrderByDescending(o => o.EntryDate);
        }

        public OrderItem CalculateItem(OrderItem item)
        {
            item.Name = item.Name ?? "Personalizado";

            foreach (var ingredient in item.Ingredients)
            {
                if (context != null)
                {
                    ingredient.Ingredient = context.Ingredients.Find(ingredient.Ingredient.Id);
                }

                ingredient.Price = ingredient.Ingredient.Price;
            }

            CalculateItemPrice(item);

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

                    // Recalculate the item's price to prevent letting the user
                    // manipulate the data to be saved.
                    CalculateItem(item);
                }
            }

            context.Orders.Add(order);

            context.SaveChanges();

            return order;
        }

        private void CalculateItemPrice(OrderItem item)
        {
            item.Price = item.Ingredients.Sum(i => i.Price * i.Ammount);

            Func<Ingredients, decimal> calculateSaleIngredient = ingredient =>
            {
                var price = item.Ingredients
                    .Where(i => i.Ingredient.Id != (short)ingredient)
                    .Sum(i => i.Price * i.Ammount);

                return price + item.Ingredients
                    .Where(i => i.Ingredient.Id == (short)ingredient)
                    .Sum(i => i.Price * (i.Ammount - i.Ammount / IngredientSaleAmmount));
            };

            // The sales are calculated according to their priority.
            // If one sale applies to one item, no other sales can be applied.
            if (item.HasIngredient(Ingredients.Alface) && !item.HasIngredient(Ingredients.Bacon))
            {
                item.Price *= 0.9m;
                item.Sale = Sales.Light;
            }
            else if (item.CountIngredient(Ingredients.Hamburguer) >= IngredientSaleAmmount)
            {
                item.Price = calculateSaleIngredient(Ingredients.Hamburguer);
                item.Sale = Sales.MuitaCarne;
            }
            else if (item.CountIngredient(Ingredients.Queijo) >= IngredientSaleAmmount)
            {
                item.Price = calculateSaleIngredient(Ingredients.Queijo);
                item.Sale = Sales.MuitoQueijo;
            }
        }
    }
}