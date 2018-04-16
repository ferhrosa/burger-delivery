using System.Collections.Generic;
using System.Linq;
using Burger.Api.Models;

namespace Burger.Api.Data
{
    public class ContextInitializer : System.Data.Entity.DropCreateDatabaseAlways<Context>
    {

        protected override void Seed(Context context)
        {
            if (!context.Ingredients.Any())
            {
                context.Ingredients.AddRange(new Ingredient[]
                {
                    new Ingredient { Id = 1, Name = "Alface", Price = 0.40m },
                    new Ingredient { Id = 2, Name = "Bacon", Price = 2.00m },
                    new Ingredient { Id = 3, Name = "Hambúrguer de carne", Price = 3.00m },
                    new Ingredient { Id = 4, Name = "Ovo", Price = 0.80m },
                    new Ingredient { Id = 5, Name = "Queijo", Price = 1.50m },
                });
                context.SaveChanges();
            }

            if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(new Recipe[]
                {
                    new Recipe { Id = 1, Name = "X-Bacon", Ingredients = new List<Ingredient>
                    {
                        context.Ingredients.Find(2), // Bacon
                        context.Ingredients.Find(3), // Hambúrguer de carne
                        context.Ingredients.Find(5), // Queijo
                    }},
                    new Recipe { Id = 2, Name = "X-Burger", Ingredients = new List<Ingredient>
                    {
                        context.Ingredients.Find(3), // Hambúrguer de carne
                        context.Ingredients.Find(5), // Queijo
                    }},
                    new Recipe { Id = 3, Name = "X-Egg", Ingredients = new List<Ingredient>
                    {
                        context.Ingredients.Find(3), // Hambúrguer de carne
                        context.Ingredients.Find(4), // Ovo
                        context.Ingredients.Find(5), // Queijo
                    }},
                    new Recipe { Id = 4, Name = "X-Egg Bacon", Ingredients = new List<Ingredient>
                    {
                        context.Ingredients.Find(2), // Bacon
                        context.Ingredients.Find(3), // Hambúrguer de carne
                        context.Ingredients.Find(4), // Ovo
                        context.Ingredients.Find(5), // Queijo
                    }},
                });
                context.SaveChanges();
            }
        }

    }
}