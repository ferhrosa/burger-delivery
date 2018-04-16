using System;
using System.Collections.Generic;
using Burger.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burger.Api.Tests
{
    [TestClass]
    public class RecipeTests
    {
        Ingredient alface = new Ingredient { Price = 0.4m };
        Ingredient bacon = new Ingredient { Price = 2 };
        Ingredient hamburguer = new Ingredient { Price = 3 };
        Ingredient ovo = new Ingredient { Price = 0.8m };
        Ingredient queijo = new Ingredient { Price = 1.5m };


        [TestMethod]
        public void PriceOfMenuRecipes()
        {
            var recipe = new Recipe();

            // X-Bacon
            recipe.Ingredients = new List<Ingredient> { bacon, hamburguer, queijo };
            Assert.AreEqual(recipe.Price, 6.5m);

            // X-Burger
            recipe.Ingredients = new List<Ingredient> { hamburguer, queijo };
            Assert.AreEqual(recipe.Price, 4.5m);

            // X-Egg
            recipe.Ingredients = new List<Ingredient> { ovo, hamburguer, queijo };
            Assert.AreEqual(recipe.Price, 5.3m);

            // X-Egg Bacon
            recipe.Ingredients = new List<Ingredient> { ovo, bacon, hamburguer, queijo };
            Assert.AreEqual(recipe.Price, 7.3m);
        }

        [TestMethod]
        public void PriceOfOherRecipes()
        {
            var recipe = new Recipe();

            recipe.Ingredients = new List<Ingredient> { hamburguer };
            Assert.AreEqual(recipe.Price, 3m);

            recipe.Ingredients = new List<Ingredient> { alface, hamburguer };
            Assert.AreEqual(recipe.Price, 3.4m);

            recipe.Ingredients = new List<Ingredient> { alface, hamburguer, ovo };
            Assert.AreEqual(recipe.Price, 4.2m);
        }

    }
}
