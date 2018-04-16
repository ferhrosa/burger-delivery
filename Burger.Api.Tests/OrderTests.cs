using System;
using System.Collections.Generic;
using Burger.Api.Models;
using Burger.Api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burger.Api.Tests
{
    [TestClass]
    public class OrderTests
    {
        Ingredient alface = new Ingredient { Id = (short)Ingredients.Alface, Price = 0.4m };
        Ingredient bacon = new Ingredient { Id = (short)Ingredients.Bacon, Price = 2 };
        Ingredient hamburguer = new Ingredient { Id = (short)Ingredients.Hamburguer, Price = 3 };
        Ingredient ovo = new Ingredient { Id = (short)Ingredients.Ovo, Price = 0.8m };
        Ingredient queijo = new Ingredient { Id = (short)Ingredients.Queijo, Price = 1.5m };

        OrderService orderService = new OrderService();


        [TestMethod]
        public void PriceOfOrder()
        {
            var order = new Order();
            order.Items = new List<OrderItem>();

            Assert.AreEqual(order.Price, 0);

            var item = new OrderItem
            {
                Ingredients = new List<OrderItemIngredient>
                {
                    new OrderItemIngredient {
                        Ingredient = hamburguer,
                        Ammount = 1,
                    },
                }
            };

            orderService.CalculateItem(item);
            order.Items.Add(item);

            Assert.AreEqual(item.Price, 3);
            Assert.AreEqual(order.Price, 3);

            var item2 = new OrderItem
            {
                Ingredients = new List<OrderItemIngredient>
                {
                    new OrderItemIngredient { Ingredient = hamburguer, Ammount = 2 },
                    new OrderItemIngredient { Ingredient = alface, Ammount = 1 },
                }
            };

            orderService.CalculateItem(item2);
            order.Items.Add(item2);

            Assert.AreEqual(item2.Price, 6.4m);
            Assert.AreEqual(order.Price, 9.4m);
        }

        [TestMethod]
        public void PriceOfSales()
        {
            var order = new Order();
            order.Items = new List<OrderItem>();

            var item = new OrderItem
            {
                Ingredients = new List<OrderItemIngredient>
                {
                    new OrderItemIngredient { Ingredient = ovo, Ammount = 1 },
                    new OrderItemIngredient { Ingredient = alface, Ammount = 1 },
                }
            };
            orderService.CalculateItem(item);
            Assert.AreEqual(item.Price, 1.08m);

            item.Ingredients.Add(new OrderItemIngredient
            {
                Ingredient = queijo,
                Ammount = 3
            });
            orderService.CalculateItem(item);
            Assert.AreEqual(item.Price, 5.13m);

            item.Ingredients.Add(new OrderItemIngredient
            {
                Ingredient = hamburguer,
                Ammount = 3
            });
            orderService.CalculateItem(item);
            Assert.AreEqual(item.Price, 13.23m);


            var item2 = new OrderItem
            {
                Ingredients = new List<OrderItemIngredient>
                {
                    new OrderItemIngredient { Ingredient = queijo, Ammount = 2 },
                }
            };
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 3m);

            item2.Ingredients[0].Ammount = 3;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 3m);

            item2.Ingredients[0].Ammount = 4;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 4.5m);

            item2.Ingredients[0].Ammount = 5;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 6m);

            item2.Ingredients[0].Ammount = 6;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 6m);

            item2.Ingredients[0].Ammount = 8;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 9m);

            item2.Ingredients[0].Ammount = 9;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 9m);

            item2.Ingredients.Add(new OrderItemIngredient
            {
                Ingredient = hamburguer,
                Ammount = 2
            });
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 15m);

            item2.Ingredients[1].Ammount = 3;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 19.5m);

            item2.Ingredients[1].Ammount = 4;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 22.5m);

            item2.Ingredients[1].Ammount = 5;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 25.5m);

            item2.Ingredients[1].Ammount = 6;
            orderService.CalculateItem(item2);
            Assert.AreEqual(item2.Price, 25.5m);
        }

    }
}
